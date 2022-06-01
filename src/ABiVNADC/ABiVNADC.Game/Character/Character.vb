Imports System.Text

Public Class Character
    Implements IInventoryHost
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub

    ReadOnly Property IsPoisoned As Boolean
        Get
            Return CharacterPoisoningData.Read(Id).Any
        End Get
    End Property

    ReadOnly Property NeedsRest As Boolean
        Get
            Return Exists AndAlso Statistic(StatisticType.Energy) < Maximum(StatisticType.Energy)
        End Get
    End Property

    Friend Sub PurgePoisons()
        CharacterPoisoningData.Clear(Id)
    End Sub

    ReadOnly Property SortOrder As Long
        Get
            Return CharacterType.SortOrder
        End Get
    End Property

    Friend Sub ThrowWeapon(builder As StringBuilder)
        If Not Equipment.ContainsKey(EquipSlot.Weapon) Then
            builder.AppendLine($"{FullName} has no weapon equipped!")
            Return
        End If
        Dim item = Equipment(EquipSlot.Weapon)
        If Not item.CanThrow Then
            builder.AppendLine($"{FullName} cannot throw {item.FullName}")
            Return
        End If
        'TODO: drop or destroy?
        'TODO: effect of throwing the item?
        Throw New NotImplementedException()
    End Sub

    Friend Sub Craft(itemType As ItemType, quantity As Long, builder As StringBuilder)
        If Not itemType.CanCraft Then
            builder.AppendLine($"{FullName} does not know how to craft {itemType.Name}")
            Return
        End If
        Dim craftCount As Long = 0
        While craftCount < quantity
            Dim recipe = itemType.FindValidRecipe(Inventory)
            If recipe Is Nothing Then
                Exit While
            End If
            Inventory.CraftRecipe(recipe)
            craftCount += 1
        End While
        builder.AppendLine($"{FullName} crafts {craftCount} {itemType.Name}.")
    End Sub

    Friend Sub Forage(builder As StringBuilder)
        If InCombat Then
            builder.AppendLine($"{FullName} cannot forage now!")
            Return
        End If
        If Not Location.CanForage Then
            builder.AppendLine($"{FullName} cannot forage here.")
            Return
        End If
        Dim result As Dictionary(Of ItemType, Long) = Location.GenerateForage()
        If Not result.Any Then
            builder.AppendLine($"{FullName} forages and finds nothing.")
            Return
        End If
        builder.Append($"{FullName} finds ")
        builder.AppendJoin(", ", result.Select(Function(x) $"{x.Key.Name}(x{x.Value})"))
        builder.AppendLine()
        For Each entry In result
            Dim quantity = entry.Value
            While quantity > 0
                quantity -= 1
                Inventory.Add(Item.Create(entry.Key))
            End While
        Next
    End Sub

    Friend Sub ApplyElementalDamage(elementalDamageType As ElementalDamageType, damage As Long, builder As StringBuilder)
        damage = CharacterType.ModifyElementalDamage(elementalDamageType, damage)
        builder.AppendLine($"{FullName} takes {damage} damage from {elementalDamageType.Name}.")
        AddWounds(damage)
        If IsDead Then
            builder.AppendLine($"{FullName} dies.")
            Destroy(True)
        End If
    End Sub

    ReadOnly Property OwnedFeatures As IEnumerable(Of Feature)
        Get
            Return FeatureOwnerData.ReadForCharacter(Id).Select(Function(id) New Feature(id))
        End Get
    End Property

    ReadOnly Property Effects As HashSet(Of EffectType)
        Get
            Return New HashSet(Of EffectType)(CharacterEffectData.ReadForCharacter(Id).Select(Function(x) CType(x, EffectType)))
        End Get
    End Property

    ReadOnly Property Encumbrance As Long
        Get
            Return Equipment.Sum(Function(x) x.Value.EquippedEncumbrance) + Inventory.InventoryEncumbrance
        End Get
    End Property

    Public Sub Drop(items As IEnumerable(Of Item), builder As StringBuilder)
        For Each item In items
            Location.Inventory.Add(item)
        Next
        builder.AppendLine($"{FullName} drops {items.Count} items.")
    End Sub

    ReadOnly Property MaximumEncumbrance As Long
        Get
            Return CharacterType.MaximumEncumbrance
        End Get
    End Property

    ReadOnly Property IsEncumbered As Boolean
        Get
            Return Encumbrance > MaximumEncumbrance
        End Get
    End Property

    Public Sub Deliver(builder As StringBuilder)
        If Not HasQuest Then
            builder.AppendLine($"{FullName} does not have a quest.")
            Return
        End If
        If Location.QuestGiver Is Nothing OrElse Location.QuestGiver.Id <> QuestGiver.Id Then
            builder.AppendLine($"The quest giver is not here.")
            Return
        End If
        If Not Inventory.HasItem(QuestGiver.TargetItemType) Then
            builder.AppendLine($"{FullName} does not have any {QuestGiver.TargetItemType.Name}!")
            Return
        End If
        Dim items = Inventory.StackedItems(QuestGiver.TargetItemType).Take(CInt(QuestGiver.TargetQuantity))
        If items.Count < QuestGiver.TargetQuantity Then
            builder.AppendLine($"{FullName} does not have enough {QuestGiver.TargetItemType.Name}!")
            Return
        End If
        builder.AppendLine($"{FullName} gives {QuestGiver.Name} the {QuestGiver.TargetQuantity} {QuestGiver.TargetItemType.Name}, and receives {QuestGiver.RewardQuantity} {QuestGiver.RewardItemType.Name}!")
        QuestGiver.CompleteQuest(Me, items)
        CharacterQuestData.Clear(Id)
    End Sub

    ReadOnly Property Exists As Boolean
        Get
            Return CharacterData.Exists(Id)
        End Get
    End Property

    Public Sub Rename(newName As String)
        If String.IsNullOrWhiteSpace(newName) Then
            newName = RNG.FromList(Names.Human)
        End If
        CharacterData.WriteName(Id, newName)
    End Sub

    ReadOnly Property FullName As String
        Get
            Return $"{Name} the {CharacterType.Name}"
        End Get
    End Property

    Public Sub AcceptQuest(questGiver As QuestGiver, builder As StringBuilder)
        If HasQuest Then
            builder.AppendLine($"{FullName} already has a quest!")
            Return
        End If
        CharacterQuestData.Write(Id, questGiver.Id)
        builder.AppendLine($"{FullName} accepts {questGiver.Name}'s quest for {questGiver.TargetQuantity} {questGiver.TargetItemType.Name}.")
    End Sub

    Public Sub BuyLandClaims(quantity As Long, builder As StringBuilder)
        If Location.LocationType <> LocationType.LandClaimOffice Then
            builder.AppendLine($"{FullName} needs to be at a land claim office to buy land claims.")
            Return
        End If
        Dim office = Location.LandClaimOffice
        Dim items = Inventory.Items.Where(Function(x) x.ItemType = ItemType.Jools).ToList
        Dim jools As Long = 0
        Dim claims As Long = 0
        While quantity > 0 AndAlso items.LongCount >= office.ClaimPrice
            quantity -= 1
            claims += 1
            jools += office.ClaimPrice
            Inventory.Add(Item.Create(ItemType.LandClaim))
            For Each item In items.Take(CInt(office.ClaimPrice))
                item.Destroy()
            Next
            office.ClaimPrice += 1
            items = Inventory.Items.Where(Function(x) x.ItemType = ItemType.Jools).ToList
        End While
        builder.AppendLine($"{FullName} buys {claims} {ItemType.LandClaim.Name} for {jools} {ItemType.Jools.Name}.")
    End Sub

    ReadOnly Property Name As String
        Get
            Return Data.CharacterData.ReadName(Id)
        End Get
    End Property

    Friend Sub NextTurn(builder As StringBuilder)
        ApplyEffects(builder)
        If InCombat Then
            Dim enemies = Location.Enemies(Me)
            For Each enemy In enemies
                If Not Exists Then
                    Continue For
                End If
                PerformCounterAttack(builder, enemy)
            Next
        End If
    End Sub

    Private Sub PerformCounterAttack(builder As StringBuilder, enemy As Character)
        builder.AppendLine("---")
        Select Case enemy.GenerateCombatAction()
            Case CombatActionType.Attack
                enemy.Attack(Me, builder)
            Case CombatActionType.Infect
                enemy.Infect(Me, builder)
            Case CombatActionType.Poison
                enemy.Poison(Me, builder)
            Case CombatActionType.Rest
                enemy.CombatRest(builder)
        End Select
        enemy.ApplyEffects(builder)
    End Sub

    Private Sub Poison(character As Character, builder As StringBuilder)
        character.AddPoison(PoisonDice)
        builder.AppendLine($"{FullName} poisons {character.FullName}!")
    End Sub

    Private Sub AddPoison(poisonDice As String)
        CharacterPoisoningData.Write(Id, poisonDice)
    End Sub

    ReadOnly Property PoisonDice As String
        Get
            Return CharacterType.PoisonDice
        End Get
    End Property

    Private Sub Infect(character As Character, builder As StringBuilder)
        Dim infection = RNG.RollDice(InfectionDice)
        character.ChangeEffectDuration(EffectType.Infection, infection)
        builder.AppendLine($"{FullName} infects {character.FullName} for {infection} turns.")
    End Sub

    ReadOnly Property InfectionDice As String
        Get
            Return CharacterType.InfectionDice
        End Get
    End Property

    Private Function GenerateCombatAction() As CombatActionType
        Return CharacterType.GenerateCombatAction(Me)
    End Function

    Friend Sub ApplyEffects(builder As StringBuilder)
        ApplyPoisoning(builder)
        If IsDead Then
            builder.AppendLine($"{FullName} is dead.")
            Destroy(True)
            Return
        End If
        For Each effect In Effects
            effect.ApplyOn(Me, builder)
            ChangeEffectDuration(effect, -1)
            If IsDead Then
                builder.AppendLine($"{FullName} is dead.")
                Destroy(True)
                Exit For
            End If
        Next
    End Sub

    Private Sub ApplyPoisoning(builder As StringBuilder)
        Dim poisonings = CharacterPoisoningData.Read(Id)
        If poisonings.Any Then
            Dim diceToRoll = String.Join("+", poisonings)
            Dim poisonDamage = RNG.RollDice(diceToRoll)
            If poisonDamage > 0 Then
                builder.AppendLine($"{FullName} suffers {poisonDamage} poison damage!")
                AddWounds(poisonDamage)
            End If
        End If
    End Sub

    Friend Sub ChangeEffectDuration(effectType As EffectType, delta As Long)
        Dim duration = If(CharacterEffectData.Read(Id, effectType), 0) + delta
        If duration > 0 Then
            CharacterEffectData.Write(Id, effectType, duration)
        Else
            CharacterEffectData.Clear(Id, effectType)
        End If
    End Sub

    ReadOnly Property HasLocation As Boolean
        Get
            Return Location IsNot Nothing
        End Get
    End Property

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

    Public Sub BribeEnemy(enemy As Character, item As Item, builder As StringBuilder)
        If item Is Nothing OrElse Not enemy.TakesBribe(item) Then
            builder.AppendLine($"{FullName} fails to bribe {enemy.FullName}.")
        End If
        builder.AppendLine($"{FullName} successfully bribes {enemy.FullName}.")
        enemy.Inventory.Add(item)
        enemy.Destroy(False)
    End Sub

    Public Function TakesBribe(item As Item) As Boolean
        Return CharacterType.TakesBribe(item.ItemType)
    End Function

    Public Sub Unequip(item As Item, builder As StringBuilder)
        CharacterEquipSlotData.ClearForItem(item.Id)
        Inventory.Add(item)
        builder.AppendLine($"{FullName} unequips {item.FullName}")
    End Sub

    Friend Function HasEffect(effectType As EffectType) As Boolean
        Return Effects.Contains(effectType)
    End Function

    ReadOnly Property AttackDice As String
        Get
            Dim result As String = If(Equipment.ContainsKey(EquipSlot.Weapon), "0d1", CharacterType.AttackDice)
            Dim attackItems = Equipment.Values.Where(Function(x) x.HasAttackDice)
            If attackItems.Any Then
                result = $"{result}+{String.Join("+"c, attackItems.Select(Function(x) x.AttackDice))}"
            End If
            For Each effect In Effects.Where(Function(x) x.HasAttackDice)
                result &= $"+{effect.AttackDice}"
            Next
            Return result
        End Get
    End Property

    Public Function RollAttack() As Long
        Dim result = RNG.RollDice(AttackDice)
        Return If(result < 0, 0, result)
    End Function

    Public Sub Equip(item As Item, builder As StringBuilder)
        If Not item.ItemType.CanEquip Then
            builder.AppendLine($"I don't know where you would equip that, and I don't think I wanna know where you'd try!")
            Return
        End If
        Dim equipSlot = item.ItemType.EquipSlot
        Dim equippedItem As Item = GetEquippedItem(equipSlot)
        If equippedItem IsNot Nothing Then
            CharacterEquipSlotData.ClearForItem(equippedItem.Id)
            Inventory.Add(equippedItem)
        End If
        Inventory.Remove(item)
        CharacterEquipSlotData.Write(Id, equipSlot, item.Id)
        builder.AppendLine($"{FullName} equips {item.FullName}.")
    End Sub

    Private Function GetEquippedItem(equipSlot As EquipSlot) As Item
        Dim itemId As Long? = CharacterEquipSlotData.Read(Id, equipSlot)
        Return Item.FromId(itemId)
    End Function

    ReadOnly Property DefendDice As String
        Get
            Dim result = CharacterType.DefendDice
            Dim defendItems = Equipment.Values.Where(Function(x) x.HasDefendDice)
            For Each defendItem In defendItems
                result &= $"+{defendItem.DefendDice}"
            Next
            For Each effect In Effects.Where(Function(x) x.HasDefendDice)
                result &= $"+{effect.DefendDice}"
            Next
            Return result
        End Get
    End Property

    Public Function RollDefend() As Long
        Dim result = RNG.RollDice(DefendDice)
        Return If(result < 0, 0, result)
    End Function

    Public Sub NonCombatRest(builder As StringBuilder)
        Dim characterType As CharacterType = If(HasLocation AndAlso Location.HasDungeon, Location.Dungeon.GenerateWanderingMonster(), CharacterType.None)
        If characterType <> CharacterType.None Then
            Dim characterId = Data.CharacterData.Create(characterType.RandomName, characterType, 0, Location.Id)
            builder.AppendLine($"Suddenly, {Character.FromId(characterId).FullName} appears!")
        Else
            AddFatigue(Statistic(StatisticType.Energy) - Maximum(StatisticType.Energy))
            builder.AppendLine($"{FullName} rests fully.")
        End If
    End Sub

    ReadOnly Property IsEnemy(candidate As Character) As Boolean
        Get
            Return Exists AndAlso CharacterType <> CharacterType.None AndAlso CharacterType.IsEnemy(candidate)
        End Get
    End Property

    ReadOnly Property Faction As Faction
        Get
            Return CharacterType.Faction
        End Get
    End Property

    Public Sub CombatRest(builder As StringBuilder)
        Dim maximumRest = Maximum(StatisticType.Energy) - Statistic(StatisticType.Energy)
        Dim restRoll = Math.Min(RNG.RollDice(CharacterType.CombatRestRoll), maximumRest)
        AddFatigue(-restRoll)
        builder.AppendLine($"{FullName} recovers {restRoll} energy.")
    End Sub

    Public Sub AddWounds(damage As Long)
        Dim newWounds = Math.Max(0, Math.Min(If(CharacterStatisticData.Read(Id, StatisticType.Health), 0) + damage, Maximum(StatisticType.Health)))
        CharacterStatisticData.Write(Id, StatisticType.Health, newWounds)
    End Sub

    Shared Function FromId(characterId As Long?) As Character
        If characterId.HasValue Then
            Return New Character(characterId.Value)
        End If
        Return Nothing
    End Function

    Public ReadOnly Property IsDead() As Boolean
        Get
            Return Statistic(StatisticType.Health) <= 0
        End Get
    End Property

    Public Sub Destroy(incentivize As Boolean)
        If HasLocation Then
            DropEquipment()
            DropInventory()
            DropLoot()
        End If
        If HasPlayer Then
            Player.AddRIP(Me, incentivize)
            Dim corpseFeature = Feature.Create(Location, FeatureType.Corpse)
            CorpseData.Create(corpseFeature.Id, FullName)
        End If
        CharacterData.Clear(Id)
        InventoryData.ClearOrphans()
    End Sub

    ReadOnly Property IncentiveValue As Long
        Get
            Return CharacterType.IncentiveValue(Me)
        End Get
    End Property

    Friend Sub ClaimLand(item As Item, builder As StringBuilder)
        If item.ItemType <> ItemType.LandClaim Then
            builder.AppendLine($"A {ItemType.LandClaim.Name} is needed to claim land!")
            Return
        End If
        If Not Location.CanClaim Then
            builder.AppendLine("This plot of land cannot be claimed.")
            Return
        End If
        Location.ForSaleSign.Destroy()
        item.Destroy()
        Location.Owner = Me
        builder.AppendLine($"{FullName} is now the owner of this plot of land.")
    End Sub

    Public ReadOnly Property Player As Player
        Get
            Dim playerId As Long? = PlayerCharacterData.ReadForCharacter(Id)
            Return If(playerId.HasValue, New Player(playerId.Value), Nothing)
        End Get
    End Property

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

    ReadOnly Property HasPlayer As Boolean
        Get
            Return Player IsNot Nothing
        End Get
    End Property

    Property Location As Location
        Get
            Dim locationId As Long? = CharacterData.ReadLocation(Id)
            If locationId.HasValue Then
                Return New Location(locationId.Value)
            End If
            Return Nothing
        End Get
        Set(value As Location)
            If value.Id <> Location.Id Then
                CharacterData.WriteLocation(Id, value.Id)
                Location.EnteredBy(Me)
            End If
        End Set
    End Property
    ReadOnly Property Inventory As Inventory Implements IInventoryHost.Inventory
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
            Return InCombat AndAlso Statistic(StatisticType.Energy) >= CharacterType.FightEnergyCost
        End Get
    End Property
    ReadOnly Property Level As Long
        Get
            Return CharacterData.ReadLevel(Id).Value
        End Get
    End Property
    ReadOnly Property EquipmentItems As IEnumerable(Of Item)
        Get
            Return Equipment.Values
        End Get
    End Property

    Function FindEquipmentItemsByName(name As String) As IEnumerable(Of Item)
        Return EquipmentItems.Where(Function(x) x.FullName = name OrElse x.ItemType.Name = name OrElse x.ItemType.Aliases.Contains(name))
    End Function

    ReadOnly Property Statistics As Dictionary(Of StatisticType, Long)
        Get
            Return StatisticTypeDescriptors.Keys.
                Where(Function(x) Maximum(x) > 0).
                ToDictionary(Function(key) key, Function(key) Statistic(key))
        End Get
    End Property

    Function Statistic(statisticType As StatisticType) As Long
        Return Maximum(statisticType) - If(CharacterStatisticData.Read(Id, statisticType), 0)
    End Function

    Function Maximum(statisticType As StatisticType) As Long
        Return CharacterType.Maximum(statisticType, Me) + EquipmentItems.Sum(Function(x) x.StatisticModifier(statisticType))
    End Function

    Sub Attack(defender As Character, builder As StringBuilder)
        Dim fatigue = CharacterType.FightEnergyCost
        AddFatigue(fatigue)
        Dim attackRoll = RollAttack()
        builder.AppendLine($"{FullName} rolls an attack of {attackRoll}!")
        For Each weaponName In ReduceWeaponDurability(attackRoll)
            builder.AppendLine($"! ! ! {FullName}'s {weaponName} breaks ! ! !")
        Next
        Dim defendRoll = defender.RollDefend
        builder.AppendLine($"{defender.FullName} rolls a defend of {defendRoll}!")
        For Each armorType In defender.ReduceArmorDurability(attackRoll)
            builder.AppendLine($"! ! ! {defender.FullName}'s {armorType.Name} breaks ! ! !")
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
            defender.Destroy(True)
        Else
            builder.AppendLine($"{defender.FullName} has {defender.Statistic(StatisticType.Health)} health left.")
        End If
    End Sub

    ReadOnly Property Experience As Long
        Get
            Return ReadExperience(Id).Value
        End Get
    End Property

    Private Function AddExperience(experiencePoints As Long) As Boolean
        Dim result = False
        Dim newExperience = Experience + experiencePoints
        While newExperience >= ExperienceGoal AndAlso Level < CharacterType.MaximumLevel
            newExperience -= ExperienceGoal
            LevelUp()
            result = True
        End While
        CharacterData.WriteExperience(Id, newExperience)
        Return result
    End Function

    Private Sub LevelUp()
        CharacterData.WriteCharacterLevel(Id, ExperienceLevel + 1)
        AddWounds(Statistic(StatisticType.Health) - Maximum(StatisticType.Health))
        AddFatigue(Statistic(StatisticType.Energy) - Maximum(StatisticType.Energy))
    End Sub

    ReadOnly Property ExperiencePointValue As Long
        Get
            Return CharacterType.ExperiencePointValue(ExperienceLevel)
        End Get
    End Property

    ReadOnly Property ExperienceLevel As Long
        Get
            Return CharacterData.ReadCharacterLevel(Id).Value
        End Get
    End Property

    ReadOnly Property ExperienceGoal As Long
        Get
            Return CharacterType.ExperienceGoal(ExperienceLevel)
        End Get
    End Property

    Public ReadOnly Property HasQuest As Boolean
        Get
            Return CharacterQuestData.ReadFeature(Id).HasValue
        End Get
    End Property

    Public ReadOnly Property QuestGiver As QuestGiver
        Get
            Return QuestGiver.FromId(CharacterQuestData.ReadFeature(Id))
        End Get
    End Property

    Private Function ReduceArmorDurability(attackRoll As Long) As IEnumerable(Of ItemType)
        Dim result As New List(Of ItemType)
        While attackRoll > 0
            Dim items = Equipment.Values.Where(Function(x) x.HasDurability(DurabilityType.Armor)).ToList
            If items.Any Then
                Dim item = RNG.FromList(items)
                Dim itemType = item.ItemType
                If item.ReduceDurability(DurabilityType.Armor, 1) Then
                    result.Add(itemType)
                End If
            End If
            attackRoll -= 1
        End While
        Return result
    End Function

    Private Function ReduceWeaponDurability(attackRoll As Long) As IEnumerable(Of String)
        Dim result As New List(Of String)
        While attackRoll > 0
            Dim items = Equipment.Values.Where(Function(x) x.HasDurability(DurabilityType.Weapon)).ToList
            If items.Any Then
                Dim item = RNG.FromList(items)
                Dim itemName = item.FullName
                If item.ReduceDurability(DurabilityType.Weapon, 1) Then 'ReductDurability may Destroy the item
                    result.Add(itemName)
                End If
            End If
            attackRoll -= 1
        End While
        Return result
    End Function

    Public Sub AddFatigue(fatigue As Long)
        Dim newFatigue = If(CharacterStatisticData.Read(Id, StatisticType.Energy), 0) + fatigue
        CharacterStatisticData.Write(Id, StatisticType.Energy, If(newFatigue < 0, 0, newFatigue))
    End Sub

    Public Shared Function Create(characterType As CharacterType, level As Long, location As Location) As Character
        Return New Character(CharacterData.Create(characterType.RandomName, characterType, level, location.Id))
    End Function
End Class
