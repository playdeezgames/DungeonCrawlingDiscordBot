Imports System.Text

Public Class Player
    ReadOnly Property Id As Long
    Sub New(playerId As Long)
        Id = playerId
    End Sub

    ReadOnly Property InCombat As Boolean
        Get
            Return If(Character?.InCombat, False)
        End Get
    End Property

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

    Public Function Run() As Boolean
        If Not InCombat Then
            Return False
        End If
        SetDirection(RNG.FromList(AllDirections))
        Dim currentLocationId = Character.Location.Id
        Move()
        Return currentLocationId <> Character.Location.Id
    End Function

    Public Function DeleteDungeon(dungeonName As String) As Boolean
        Dim dungeon = Dungeons.FirstOrDefault(Function(x) x.Name = dungeonName)
        If dungeon IsNot Nothing Then
            DungeonData.Clear(dungeon.Id)
            Return True
        End If
        Return False
    End Function

    Public Function DeleteCharacter(characterName As String) As Boolean
        Dim character = Characters.FirstOrDefault(Function(x) x.Name = characterName)
        If character IsNot Nothing Then
            Data.CharacterData.Clear(character.Id)
            Return True
        End If
        Return False
    End Function

    Public Sub TurnAround()
        If CanTurn Then
            SetDirection(AheadDirection.Value.OppositeDirection)
        End If
    End Sub

    Public Function UseItem(item As Item) As String
        Select Case item.ItemType
            Case ItemType.Food
                Return UseFood(item)
            Case ItemType.Potion
                Return UsePotion(item)
            Case ItemType.Dagger
                Return UseDagger()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function UseDagger() As String
        Dim result = ItemType.Dagger.UseMessage(Character.FullName)
        Character.Destroy()
        Return result
    End Function

    Private Function UsePotion(item As Item) As String
        Const PotionWoundRecovery As Long = 4
        Dim builder As New StringBuilder
        builder.AppendLine(ItemType.Potion.UseMessage(Character.FullName))
        Character.AddWounds(-PotionWoundRecovery)
        builder.Append($"{Character.FullName} now has {Character.Health} health.")
        item.Destroy()
        Return builder.ToString
    End Function

    Private Function UseFood(item As Item) As String
        Const FoodFatigueRecovery As Long = 4
        Dim builder As New StringBuilder
        builder.AppendLine(ItemType.Food.UseMessage(Character.FullName))
        Character.AddFatigue(-FoodFatigueRecovery)
        builder.Append($"{Character.FullName} now has {Character.Energy} energy.")
        item.Destroy()
        Return builder.ToString
    End Function

    Public Sub TurnLeft()
        If CanTurn Then
            SetDirection(AheadDirection.Value.LeftDirection)
        End If
    End Sub

    Public Sub TurnRight()
        If CanTurn Then
            SetDirection(AheadDirection.Value.RightDirection)
        End If
    End Sub

    Public Sub Move()
        Select Case If(Character?.Location?.LocationType, LocationType.None)
            Case LocationType.Dungeon
                DungeonMove(Character, AheadDirection.Value)
            Case LocationType.Overworld
                OverworldMove(Character, AheadDirection.Value)
        End Select
    End Sub

    Private Shared Sub OverworldMove(character As Character, direction As Direction)
        Dim walker = DirectionWalker(direction)
        Dim location = character.Location
        Dim nextX = location.OverworldX.Value + walker.DeltaX
        Dim nextY = location.OverworldY.Value + walker.DeltaY
        character.Location = Location.AutogenerateOverworldXY(nextX, nextY)
    End Sub

    Private Shared Sub DungeonMove(character As Character, direction As Direction)
        Dim route As Route = Nothing
        If character.Location.Routes.TryGetValue(direction, route) Then
            character.Location = route.ToLocation
        End If
    End Sub

    Public Sub SetDirection(newDirection As Direction)
        PlayerCharacterData.WriteDirectionForPlayer(Id, newDirection)
    End Sub

    Public Sub CreateCharacter()
        If Not HasCharacter Then
            CreateCharacter(RNG.FromList(Names.Human))
            Dim x As Long = RNG.FromRange(Short.MinValue, Short.MaxValue)
            Dim y As Long = RNG.FromRange(Short.MinValue, Short.MaxValue)
            Character.Location = Game.Location.AutogenerateOverworldXY(x, y)
        End If
    End Sub

    Public Function CreateCharacter(characterName As String) As Boolean
        If PlayerCharacterData.ReadCountForPlayerAndCharacterName(Id, characterName) = 0 Then
            Dim characterId = Data.CharacterData.Create(characterName, CharacterType.N00b, 0)
            PlayerCharacterData.Write(Id, characterId, RNG.FromList(AllDirections))
            If Character Is Nothing Then
                SwitchCharacter(characterName)
            End If
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

    Friend Sub AddRIP(character As Character)
        PlayerRIPData.Write(Id, $"{character.FullName}(Level: {character.Level})")
    End Sub

    ReadOnly Property Dungeons As IEnumerable(Of Dungeon)
        Get
            Return DungeonData.ReadForPlayer(Id).Select(Function(id) Dungeon.FromId(id))
        End Get
    End Property

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
            Return CanTurn
        End Get
    End Property

    ReadOnly Property RIPs As IEnumerable(Of String)
        Get
            Return PlayerRIPData.ReadTombstoneTexts(Id)
        End Get
    End Property
End Class
