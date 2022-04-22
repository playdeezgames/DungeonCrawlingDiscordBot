Public Class Player
    ReadOnly Property Id As Long
    Sub New(playerId As Long)
        Id = playerId
    End Sub
    ReadOnly Property Character As Character
        Get
            Return Character.FromId(PlayerData.ReadCharacter(Id))
        End Get
    End Property

    Public Function CreateDungeon(dungeonName As String) As Boolean
        Const MazeColumns = 4
        Const MazeRows = 4
        If DungeonData.ReadCountForPlayerAndDungeonName(Id, dungeonName) = 0 Then
            Dim maze As New Maze(Of Direction)(MazeColumns, MazeRows, DirectionWalker)
            maze.Generate()
            Dim locationIds As New List(Of Long)
            While locationIds.Count < MazeColumns * MazeRows
                locationIds.Add(LocationData.Create())
            End While
            For column = 0 To maze.Columns - 1
                For row = 0 To maze.Rows - 1
                    Dim cell = maze.GetCell(column, row)
                    Dim locationId = locationIds(CInt(row * maze.Columns + column))
                    For Each direction In AllDirections
                        Dim door = cell.GetDoor(direction)
                        If door IsNot Nothing AndAlso door.Open Then
                            Dim walkStep = DirectionWalker(direction)
                            Dim nextColumn = column + walkStep.DeltaX
                            Dim nextRow = row + walkStep.DeltaY
                            Dim nextLocationId = locationIds(CInt(nextRow * maze.Columns + nextColumn))
                            RouteData.Create(locationId, direction, nextLocationId)
                        End If
                    Next
                Next
            Next
            DungeonData.Create(Id, dungeonName, locationIds(0))
            Return True
        End If
        Return False
    End Function

    Public Function CreateCharacter(characterName As String) As Boolean
        If PlayerCharacterData.ReadCountForPlayerAndCharacterName(Id, characterName) = 0 Then
            Dim characterId = CharacterData.Create(characterName)
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

    ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return PlayerCharacterData.ReadForPlayer(Id).Select(Function(id) Character.FromId(id))
        End Get
    End Property
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
End Class
