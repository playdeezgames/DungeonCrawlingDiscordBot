Imports System.Text

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub

    ReadOnly Property Exists As Boolean
        Get
            Return CharacterData.Exists(Id)
        End Get
    End Property
    ReadOnly Property FullName As String
        Get
            Return $"{Name} the {CharacterType.Name}"
        End Get
    End Property
    ReadOnly Property Name As String
        Get
            Return Data.CharacterData.ReadName(Id)
        End Get
    End Property
    ReadOnly Property HasLocation As Boolean
        Get
            Return Location IsNot Nothing
        End Get
    End Property

    Public Function Create(characterType As CharacterType, level As Long) As Character
        Return New Character(CharacterData.Create(characterType.RandomName, characterType, level))
    End Function

    ReadOnly Property Equipment As Dictionary(Of EquipSlot, Item)
        Get
            Dim equippedItemIds As Dictionary(Of Long, Long) = CharacterEquipSlotData.ReadForCharacter(Id)
            Dim result As New Dictionary(Of EquipSlot, Item)
            For Each entry In equippedItemIds
                result(CType(entry.Key, EquipSlot)) = New Item(entry.Value)
            Next
            Return result
        End Get
    End Property

    Public Function BribeEnemy(enemy As Character, itemType As ItemType) As String
        Dim item = Inventory.Items.FirstOrDefault(Function(x) x.ItemType = itemType)
        If item Is Nothing OrElse Not enemy.TakesBribe(itemType) Then
            Return $"{FullName} fails to bribe {enemy.FullName}."
        End If
        Dim result = $"{FullName} successfully bribes {enemy.FullName}."
        enemy.Inventory.Add(item)
        CharacterLocationData.Clear(enemy.Id)
        Return result
    End Function

    Public Function TakesBribe(itemType As ItemType) As Boolean
        Return CharacterType.TakesBribe(itemType)
    End Function

    Public Function Unequip(item As Item) As String
        CharacterEquipSlotData.ClearForItem(item.Id)
        Inventory.Add(item)
        Return $"{FullName} unequips {item.Name}"
    End Function

    ReadOnly Property AttackDice As String
        Get
            Dim attackItems = Equipment.Values.Where(Function(x) x.HasAttackDice)
            If attackItems.Any Then
                Return String.Join("+"c, attackItems.Select(Function(x) x.AttackDice))
            Else
                Return CharacterType.AttackDice
            End If
        End Get
    End Property

    Public Function RollAttack() As Long
        Return RNG.RollDice(AttackDice)
    End Function

    Public Function Equip(itemType As ItemType) As String
        Dim itemStacks = Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            Return $"{FullName} does not have any `{itemType.Name}` in their inventory."
        End If
        If Not itemType.CanEquip Then
            Return $"I don't know where you would equip that, and I don't think I wanna know where you'd try!"
        End If
        Dim item = itemStacks(itemType).First
        Dim equipSlot = itemType.EquipSlot
        Dim equippedItem As Item = GetEquippedItem(equipSlot)
        If equippedItem IsNot Nothing Then
            CharacterEquipSlotData.ClearForItem(equippedItem.Id)
            Inventory.Add(equippedItem)
        End If
        Inventory.Remove(item)
        CharacterEquipSlotData.Write(Id, equipSlot, item.Id)
        Return $"{FullName} equips {itemType.Name}."
    End Function

    Private Function GetEquippedItem(equipSlot As EquipSlot) As Item
        Dim itemId As Long? = CharacterEquipSlotData.Read(Id, equipSlot)
        Return Item.FromId(itemId)
    End Function

    ReadOnly Property DefendDice As String
        Get
            Dim defendItems = Equipment.Values.Where(Function(x) x.HasDefendDice)
            If defendItems.Any Then
                Return $"{CharacterType.DefendDice}+{String.Join("+"c, defendItems.Select(Function(x) x.DefendDice))}"
            Else
                Return CharacterType.DefendDice
            End If
        End Get
    End Property

    Public Function RollDefend() As Long
        Return RNG.RollDice(DefendDice)
    End Function

    Public Function NonCombatRest() As String
        Dim characterType As CharacterType = If(HasLocation, Location.Dungeon.GenerateWanderingMonster(), CharacterType.None)
        If characterType <> CharacterType.None Then
            Dim characterId = Data.CharacterData.Create(characterType.RandomName, characterType, 0)
            CharacterLocationData.Write(characterId, Location.Id)
            Return $"Suddenly, {Character.FromId(characterId).FullName} appears!"
        Else
            AddFatigue(Energy - MaximumEnergy)
            Return $"{FullName} rests fully."
        End If
    End Function

    ReadOnly Property IsEnemy As Boolean
        Get
            Return CharacterType.IsEnemy
        End Get
    End Property

    Public Function CombatRest() As Long
        Dim maximumRest = MaximumEnergy - Energy
        Dim restRoll = Math.Min(RNG.RollDice(CharacterType.CombatRestRoll), maximumRest)
        AddFatigue(-restRoll)
        Return restRoll
    End Function

    Public Sub AddWounds(damage As Long)
        Dim newWounds = Math.Max(0, Math.Min(CharacterData.ReadWounds(Id).Value + damage, MaximumHealth))
        CharacterData.WriteWounds(Id, newWounds)
    End Sub

    Shared Function FromId(characterId As Long?) As Character
        If characterId.HasValue Then
            Return New Character(characterId.Value)
        End If
        Return Nothing
    End Function

    Public ReadOnly Property IsDead() As Boolean
        Get
            Return Health <= 0
        End Get
    End Property

    Public Sub Destroy()
        If HasLocation Then
            DropEquipment()
            DropInventory()
            DropLoot()
        End If
        CharacterData.Clear(Id)
    End Sub

    Private Sub DropLoot()
        Dim lootDrops = CharacterType.LootDrops
        Dim inventory = Location.Inventory
        For Each entry In lootDrops
            Dim counter = RNG.RollDice(entry.Value)
            While counter > 0
                counter -= 1
                inventory.Add(Item.Create(entry.Key))
            End While
        Next
    End Sub

    Private Sub DropInventory()
        For Each item In Inventory.Items
            Location.Inventory.Add(item)
        Next
    End Sub

    Private Sub DropEquipment()
        For Each item In Equipment.Values
            Location.Inventory.Add(item)
        Next
    End Sub

    Property Location As Location
        Get
            Dim locationId As Long? = CharacterLocationData.Read(Id)
            If locationId.HasValue Then
                Return New Location(locationId.Value)
            End If
            Return Nothing
        End Get
        Set(value As Location)
            If value Is Nothing Then
                CharacterLocationData.Clear(Id)
            Else
                CharacterLocationData.Write(Id, value.Id)
            End If
        End Set
    End Property
    ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId As Long? = CharacterInventoryData.ReadForCharacter(Id)
            If Not inventoryId.HasValue Then
                inventoryId = InventoryData.Create()
                CharacterInventoryData.Write(Id, inventoryId.Value)
            End If
            Return New Inventory(inventoryId.Value)
        End Get
    End Property
    ReadOnly Property CharacterType As CharacterType
        Get
            Dim result = ReadCharacterType(Id)
            If Not result.HasValue Then
                Return CharacterType.None
            End If
            Return CType(result.Value, CharacterType)
        End Get
    End Property

    ReadOnly Property InCombat As Boolean
        Get
            Return If(Location?.HasEnemies(Me), False)
        End Get
    End Property
    ReadOnly Property CanFight As Boolean
        Get
            Return InCombat AndAlso Energy >= CharacterType.FightEnergyCost
        End Get
    End Property
    ReadOnly Property Health As Long
        Get
            Return MaximumHealth - ReadWounds(Id).Value
        End Get
    End Property
    ReadOnly Property Energy As Long
        Get
            Return MaximumEnergy - CharacterData.ReadFatigue(Id).Value
        End Get
    End Property
    ReadOnly Property Level As Long
        Get
            Return Data.ReadLevel(Id).Value
        End Get
    End Property
    ReadOnly Property MaximumHealth As Long
        Get
            Return CharacterType.MaximumHealth(Level)
        End Get
    End Property
    ReadOnly Property MaximumEnergy As Long
        Get
            Return CharacterType.MaximumEnergy(Level)
        End Get
    End Property
    Function Attack(defender As Character) As String
        Dim builder As New StringBuilder
        Dim fatigue = CharacterType.FightEnergyCost
        AddFatigue(fatigue)
        Dim attackRoll = RollAttack()
        builder.AppendLine($"{FullName} rolls an attack of {attackRoll}!")
        For Each weaponType In ReduceWeaponDurability(attackRoll)
            builder.AppendLine($"{FullName}'s {weaponType.Name} breaks!")
        Next
        Dim defendRoll = defender.RollDefend
        builder.AppendLine($"{defender.FullName} rolls a defend of {defendRoll}!")
        For Each armorType In defender.ReduceArmorDurability(defendRoll)
            builder.AppendLine($"{defender.FullName}'s {armorType.Name} breaks!")
        Next
        Dim damageRoll = If(attackRoll > defendRoll, attackRoll - defendRoll, 0)
        If damageRoll > 0 Then
            defender.AddWounds(damageRoll)
            builder.AppendLine($"{FullName} hits!")
            builder.AppendLine($"{defender.FullName} takes {damageRoll} damage!")
        Else
            builder.AppendLine($"{FullName} misses!")
        End If
        If defender.IsDead Then
            builder.AppendLine($"{FullName} kills {defender.FullName}")
            If AddExperience(defender.ExperiencePointValue) Then
                builder.AppendLine($"{FullName} is now level {ExperienceLevel}!")
            End If
            defender.Destroy()
        Else
            builder.AppendLine($"{defender.FullName} has {defender.Health} health left.")
        End If
        Return builder.ToString
    End Function

    ReadOnly Property Experience As Long
        Get
            Return ReadExperience(Id).Value
        End Get
    End Property

    Private Function AddExperience(experiencePoints As Long) As Boolean
        Dim result = False
        Dim newExperience = Experience + experiencePoints
        While newExperience >= ExperienceGoal
            newExperience -= ExperienceGoal
            LevelUp()
            result = True
        End While
        CharacterData.WriteExperience(Id, newExperience)
        Return result
    End Function

    Private Sub LevelUp()
        CharacterData.WriteCharacterLevel(Id, ExperienceLevel + 1)
        AddWounds(Health - MaximumHealth)
        AddFatigue(Energy - MaximumEnergy)
    End Sub

    ReadOnly Property ExperiencePointValue As Long
        Get
            Return CharacterType.ExperiencePointValue(ExperienceLevel)
        End Get
    End Property

    ReadOnly Property ExperienceLevel As Long
        Get
            Return CharacterData.ReadCharacterLevel(Id).value
        End Get
    End Property

    ReadOnly Property ExperienceGoal As Long
        Get
            Return CharacterType.ExperienceGoal(ExperienceLevel)
        End Get
    End Property

    Private Function ReduceArmorDurability(defendRoll As Long) As IEnumerable(Of ItemType)
        Dim result As New List(Of ItemType)
        While defendRoll > 0
            Dim items = Equipment.Values.Where(Function(x) x.HasArmorDurability).ToList
            If items.Any Then
                Dim item = RNG.FromList(items)
                Dim itemType = item.ItemType
                If item.ReduceArmorDurability(1) Then
                    result.Add(itemType)
                End If
            End If
            defendRoll -= 1
        End While
        Return result
    End Function

    Private Function ReduceWeaponDurability(attackRoll As Long) As IEnumerable(Of ItemType)
        Dim result As New List(Of ItemType)
        While attackRoll > 0
            Dim items = Equipment.Values.Where(Function(x) x.HasWeaponDurability).ToList
            If items.Any Then
                Dim item = RNG.FromList(items)
                Dim itemType = item.ItemType
                If item.ReduceWeaponDurability(1) Then
                    result.Add(itemType)
                End If
            End If
            attackRoll -= 1
        End While
        Return result
    End Function

    Public Sub AddFatigue(fatigue As Long)
        CharacterData.WriteFatigue(Id, CharacterData.ReadFatigue(Id).Value + fatigue)
    End Sub
End Class
