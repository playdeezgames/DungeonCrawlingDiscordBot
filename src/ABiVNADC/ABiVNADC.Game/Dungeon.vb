Public Class Dungeon
    ReadOnly Property Id As Long
    Sub New(dungeonId As Long)
        Id = dungeonId
    End Sub
    Shared Function FromId(dungeonId As Long?) As Dungeon
        If dungeonId.HasValue Then
            Return New Dungeon(dungeonId.Value)
        End If
        Return Nothing
    End Function
    ReadOnly Property Name As String
        Get
            Return DungeonData.ReadName(Id)
        End Get
    End Property
    ReadOnly Property StartingLocation As Location
        Get
            Return New Location(DungeonData.ReadLocation(Id).Value)
        End Get
    End Property
    Shared Sub Create(player As Player, dungeonName As String, mazeColumns As Long, mazeRows As Long, difficulty As Difficulty)
        Dim maze As Maze(Of Direction) = CreateMaze(mazeColumns, mazeRows)
        Dim locationIds As List(Of Long) = CreateLocations(maze)
        PopulateDoors(maze, locationIds)
        PopulateItems(locationIds, difficulty)
        PopulateCreatures(locationIds, difficulty)
        CreateDungeon(player, dungeonName, locationIds, difficulty)
    End Sub

    Private Shared Sub PopulateCreatures(locationIds As List(Of Long), difficulty As Difficulty)
        For Each characterType In AllCharacterTypes
            Dim spawnCount = characterType.SpawnCount(locationIds.LongCount, difficulty)
            While spawnCount > 0
                Dim characterId = Data.CharacterData.Create(characterType.RandomName, characterType, 0)
                CharacterLocationData.Write(characterId, RNG.FromList(locationIds))
                spawnCount -= 1
            End While
        Next
    End Sub

    Private Shared Sub PopulateItems(locationIds As List(Of Long), difficulty As Difficulty)
        For Each itemType In AllItemTypes
            Dim spawnCount = RNG.RollDice(itemType.SpawnCount(locationIds.LongCount, difficulty))
            While spawnCount > 0
                Dim location = New Location(RNG.FromList(locationIds))
                location.Inventory.Add(Item.Create(itemType))
                spawnCount -= 1
            End While
        Next
    End Sub

    Private Shared Sub CreateDungeon(player As Player, dungeonName As String, locationIds As List(Of Long), difficulty As Difficulty)
        Dim dungeonId = DungeonData.Create(player.Id, dungeonName, locationIds(0), difficulty)
        For Each locationId In locationIds
            DungeonLocationData.Write(dungeonId, locationId)
        Next
    End Sub

    Friend Function GenerateWanderingMonster() As CharacterType
        Return RNG.FromGenerator(Difficulty.WanderingMonsterTable)
    End Function

    ReadOnly Property Difficulty As Difficulty
        Get
            Return CType(DungeonData.ReadDifficulty(Id).Value, Difficulty)
        End Get
    End Property

    Private Shared Sub PopulateDoors(maze As Maze(Of Direction), locationIds As List(Of Long))
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
    End Sub

    Private Shared Function CreateMaze(mazeColumns As Long, mazeRows As Long) As Maze(Of Direction)
        Dim maze As New Maze(Of Direction)(mazeColumns, mazeRows, DirectionWalker)
        maze.Generate()
        Return maze
    End Function

    Private Shared Function CreateLocations(maze As Maze(Of Direction)) As List(Of Long)
        Dim locationIds As New List(Of Long)
        While locationIds.Count < maze.Columns * maze.Rows
            locationIds.Add(LocationData.Create())
        End While

        Return locationIds
    End Function
End Class
