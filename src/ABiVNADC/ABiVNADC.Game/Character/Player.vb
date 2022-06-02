Imports System.Text

Public Class Player
    ReadOnly Property Id As Long
    Sub New(playerId As Long)
        Id = playerId
    End Sub
    ReadOnly Property Incentives As Dictionary(Of IncentiveType, Long)
        Get
            Return PlayerIncentiveLevelData.
                ReadForPlayer(Id).
                ToDictionary(
                    Function(x) CType(x.Key, IncentiveType),
                    Function(x) x.Value)
        End Get
    End Property

    Public Sub ThrowWeapon(builder As StringBuilder)
        If Not HasCharacter Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        Character.ThrowWeapon(builder)
        Character.NextTurn(builder)
    End Sub

    Public Sub Craft(itemType As ItemType, quantity As Long, builder As StringBuilder)
        If Not HasCharacter Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        Character.Craft(itemType, quantity, builder)
        Character.NextTurn(builder)
    End Sub

    Public Sub Forage(builder As StringBuilder)
        If Not HasCharacter Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        Character.Forage(builder)
        Character.NextTurn(builder)
    End Sub

    ReadOnly Property InCombat As Boolean
        Get
            Return If(Character?.InCombat, False)
        End Get
    End Property

    ReadOnly Property IncentivePoints As Long
        Get
            Return If(PlayerIncentiveData.Read(Id), 0)
        End Get
    End Property

    Public Sub Fight(builder As StringBuilder)
        If Character Is Nothing Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        If Not Character.InCombat Then
            builder.AppendLine($"{Character.FullName} is not in combat.")
            Return
        End If
        If Not Character.CanFight Then
            builder.AppendLine($"{Character.FullName} needs to recover energy.")
            Return
        End If
        Dim enemy = Character.Location.Enemy(Character)
        Attack(enemy, builder)
    End Sub

    Public Sub Rest(builder As StringBuilder)
        If Character Is Nothing Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        If Not Character.NeedsRest Then
            builder.AppendLine($"{Character.FullName} is ready for action!")
            Return
        End If
        If InCombat Then
            CombatRest(builder)
            Return
        End If
        If Not Character.Location.CanRest Then
            builder.AppendLine($"{Character.FullName} cannot rest here.")
            Return
        End If
        Character.NonCombatRest(builder)
    End Sub

    ReadOnly Property CanFight As Boolean
        Get
            Return If(Character?.CanFight, False)
        End Get
    End Property

    ReadOnly Property Character As Character
        Get
            Return Character.FromId(PlayerData.ReadCharacter(Id))
        End Get
    End Property

    Public Function Run(builder As StringBuilder) As Boolean
        If AttemptRun(builder) Then
            builder.AppendLine($"{Character.FullName} runs!")
            Return True
        End If
        builder.AppendLine($"{Character.FullName} could not get away.")
        Character.NextTurn(builder)
        Return False
    End Function

    Public Function IncentiveLevel(incentiveType As IncentiveType) As Long
        Return If(PlayerIncentiveLevelData.Read(Id, incentiveType), 0)
    End Function

    Public Sub BuyIncentive(incentiveType As IncentiveType, builder As StringBuilder)
        If incentiveType = IncentiveType.None Then
            builder.AppendLine("Unknown incentive type!")
            Return
        End If
        Dim level = IncentiveLevel(incentiveType)
        Dim price = incentiveType.IncentivePrice(level)
        Dim available = IncentivePoints
        If price > available Then
            builder.AppendLine($"You only have {available} incentive points!")
            Return
        End If
        level += 1
        PlayerIncentiveData.Write(Id, available - price)
        PlayerIncentiveLevelData.Write(Id, incentiveType, level)
        builder.AppendLine($"You now have `{incentiveType.Name}` level {level}!")
    End Sub

    Public Sub Equip(item As Item, builder As StringBuilder)
        Character.Equip(item, builder)
        Character.NextTurn(builder)
    End Sub

    Public Sub Take(items As IEnumerable(Of Item), builder As StringBuilder)
        Dim owner = Character.Location.Owner
        If owner IsNot Nothing AndAlso owner.Id <> Character.Id Then
            builder.AppendLine("Only the owner of a plot of land may take items from it.")
            Return
        End If
        Dim namedStacks = items.GroupBy(Function(x) x.FullName)
        builder.Append($"{Character.FullName} picks up ")
        builder.AppendJoin(", ", namedStacks.Select(Function(x) $"{x.Key}(x{x.Count})"))
        builder.AppendLine(".")
        For Each item In items
            Character.Inventory.Add(item)
        Next
        Character.NextTurn(builder)
    End Sub

    Public Sub Take(itemType As ItemType, quantity As Long, builder As StringBuilder)
        Dim owner = Character.Location.Owner
        If owner IsNot Nothing AndAlso owner.Id <> Character.Id Then
            builder.AppendLine("Only the owner of a plot of land may take items from it.")
            Return
        End If
        Dim itemStacks = Character.Location.Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            builder.AppendLine($"There ain't any `{itemType.Name}` in sight.")
            Return
        End If
        Dim items = itemStacks(itemType).Take(CInt(quantity))
        quantity = items.LongCount
        For Each item In items
            Character.Inventory.Add(item)
        Next
        builder.AppendLine($"{Character.FullName} picks up {quantity} {itemType.Name}")
        Character.NextTurn(builder)
    End Sub

    Public Sub TakeTrophies(builder As StringBuilder)
        Dim owner = Character.Location.Owner
        If owner IsNot Nothing AndAlso owner.Id <> Character.Id Then
            builder.AppendLine("Only the owner of a plot of land may take items from it.")
            Return
        End If
        Dim items = Character.Location.Inventory.Items.Where(Function(x) x.IsTrophy)
        If Not items.Any Then
            builder.AppendLine("There are no trophies to take!")
            Return
        End If
        For Each item In items
            Character.Inventory.Add(item)
        Next
        builder.AppendLine($"{Character.FullName} takes all trophies.")
        Character.NextTurn(builder)
    End Sub

    Public Sub TakeAll(builder As StringBuilder)
        Dim owner = Character.Location.Owner
        If owner IsNot Nothing AndAlso owner.Id <> Character.Id Then
            builder.AppendLine("Only the owner of a plot of land may take items from it.")
            Return
        End If
        If Character.Location.Inventory.IsEmpty Then
            builder.AppendLine("There's nothing to take!")
            Return
        End If
        For Each item In Character.Location.Inventory.Items
            Character.Inventory.Add(item)
        Next
        builder.AppendLine($"{Character.FullName} takes everything.")
        Character.NextTurn(builder)
    End Sub

    Private Function AttemptRun(builder As StringBuilder) As Boolean
        If Not InCombat Then
            Return False
        End If
        SetDirection(RNG.FromList(AllCardinalDirections.ToList))
        Dim currentLocationId = Character.Location.Id
        Move(builder)
        Return currentLocationId <> Character.Location.Id
    End Function



    Public Sub Unequip(item As Item, builder As StringBuilder)
        Character.Unequip(item, builder)
        Character.NextTurn(builder)
    End Sub

    Public Sub Attack(enemy As Character, builder As StringBuilder)
        Character.Attack(enemy, builder)
        Character.NextTurn(builder)
    End Sub

    Public Sub CombatRest(builder As StringBuilder)
        Character.CombatRest(builder)
        Character.NextTurn(builder)
    End Sub

    Public Function DeleteCharacter(characterName As String) As Boolean
        Dim character = Characters.FirstOrDefault(Function(x) x.Name = characterName)
        If character IsNot Nothing Then
            Data.CharacterData.Clear(character.Id)
            Return True
        End If
        Return False
    End Function

    Public Sub TurnAround(builder As StringBuilder)
        If Not CanTurn Then
            builder.AppendLine("You cannot do that now!")
            Return
        End If
        SetDirection(AheadDirection.Value.OppositeDirection)
        If Character.HasEffect(EffectType.Nausea) AndAlso RNG.RollDice("1d2/2") > 0 Then
            builder.AppendLine($"When {Character.FullName} spins around, they get sick and vomit on the floor.")
            Feature.Create(Character.Location, FeatureType.VomitPuddle)
        End If
    End Sub

    Public Sub BribeEnemy(enemy As Character, item As Item, builder As StringBuilder)
        Character.BribeEnemy(enemy, item, builder)
        Character.NextTurn(builder)
    End Sub

    Public Sub UseItem(item As Item, builder As StringBuilder)
        If Not item.CanUse Then
            builder.AppendLine($"Cannot use `{item.FullName}`.")
            Return
        End If
        item.ItemType.OnUse(Character, item, builder)
        Character.NextTurn(builder)
    End Sub

    Public Sub TurnLeft(builder As StringBuilder)
        If Not CanTurn Then
            builder.AppendLine("You cannot do that now!")
            Return
        End If
        SetDirection(AheadDirection.Value.LeftDirection)
    End Sub

    Public Sub TurnRight(builder As StringBuilder)
        If Not CanTurn Then
            builder.AppendLine("You cannot do that now!")
            Return
        End If
        SetDirection(AheadDirection.Value.RightDirection)
    End Sub

    Public Sub Move(builder As StringBuilder)
        Select Case If(Character?.Location?.LocationType, LocationType.None)
            Case LocationType.Dungeon
                DungeonMove(Character, AheadDirection.Value, builder)
            Case LocationType.Overworld
                OverworldMove(Character, AheadDirection.Value, builder)
        End Select
    End Sub

    Private Shared Sub OverworldMove(character As Character, direction As Direction, builder As StringBuilder)
        Dim walker = DirectionWalker(direction)
        Dim location = character.Location
        Dim nextX = location.Overworld.X + walker.DeltaX
        Dim nextY = location.Overworld.Y + walker.DeltaY
        character.Location = Location.AutogenerateOverworldXY(nextX, nextY)
        character.ApplyEffects(builder)
    End Sub

    Private Shared Sub DungeonMove(character As Character, direction As Direction, builder As StringBuilder)
        Dim route As Route = Nothing
        If character.Location.Routes.TryGetValue(direction, route) Then
            character.Location = route.ToLocation
            character.ApplyEffects(builder)
        End If
    End Sub

    Public Sub SetDirection(newDirection As Direction)
        PlayerCharacterData.WriteDirectionForPlayer(Id, newDirection)
    End Sub

    Public Sub CreateCharacter()
        If Not HasCharacter Then
            Dim x As Long = RNG.FromRange(Short.MinValue, Short.MaxValue)
            Dim y As Long = RNG.FromRange(Short.MinValue, Short.MaxValue)
            CreateCharacter(RNG.FromList(Names.Human), Game.Location.AutogenerateOverworldXY(x, y))
        End If
    End Sub

    Public Function CreateCharacter(characterName As String, location As Location) As Boolean
        If PlayerCharacterData.ReadCountForPlayerAndCharacterName(Id, characterName) = 0 Then
            Dim characterId = Data.CharacterData.Create(characterName, CharacterType.N00b, 0, location.Id)
            PlayerCharacterData.Write(Id, characterId, RNG.FromEnumerable(AllCardinalDirections))
            If Character Is Nothing Then
                SwitchCharacter(characterName)
            End If
            For Each entry In Incentives
                entry.Key.ApplyTo(Character, entry.Value)
            Next
            Return True
        End If
        Return False
    End Function

    Public Function SwitchCharacter(characterName As String) As Boolean
        Dim characterIds = PlayerCharacterData.ReadForPlayerAndCharacterName(Id, characterName)
        If characterIds.Any Then
            PlayerData.Write(Id, characterIds.First)
            Return True
        End If
        Return False
    End Function

    ReadOnly Property HasCharacter As Boolean
        Get
            Return Character IsNot Nothing
        End Get
    End Property

    ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return PlayerCharacterData.ReadForPlayer(Id).Select(Function(id) Character.FromId(id))
        End Get
    End Property

    Friend Sub AddRIP(character As Character, incentivize As Boolean)
        PlayerRIPData.Write(Id, $"{character.FullName}(Level: {character.Level})")
        If incentivize Then
            PlayerIncentiveData.Write(Id, If(PlayerIncentiveData.Read(Id), 0) + character.IncentiveValue)
        End If
    End Sub

    ReadOnly Property AheadDirection As Direction?
        Get
            Dim result As Long? = PlayerCharacterData.ReadDirection(Id)
            If result IsNot Nothing Then
                Return CType(result.Value, Direction)
            End If
            Return Nothing
        End Get
    End Property
    ReadOnly Property CanTurn As Boolean
        Get
            If Not AheadDirection.HasValue OrElse Character.Location.HasEnemies(Character) Then
                Return False
            End If
            Return True
        End Get
    End Property
    ReadOnly Property CanMove As Boolean
        Get
            Return CanTurn AndAlso Not Character.IsEncumbered
        End Get
    End Property

    ReadOnly Property RIPs As IEnumerable(Of String)
        Get
            Return PlayerRIPData.ReadTombstoneTexts(Id)
        End Get
    End Property
End Class
