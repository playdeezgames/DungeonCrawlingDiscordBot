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
            Return New Location(DungeonData.ReadStartingLocation(Id).Value)
        End Get
    End Property
    Private Shared ReadOnly themeGenerator As New Dictionary(Of DungeonTheme, Integer) From
        {
            {DungeonTheme.Sewers, 16},
            {DungeonTheme.Dungeon, 2},
            {DungeonTheme.Crypt, 8},
            {DungeonTheme.Ruins, 4},
            {DungeonTheme.Cavern, 1}
        }
    Shared Function Create(dungeonName As String, overworldLocation As Location, mazeColumns As Long, mazeRows As Long, difficulty As Difficulty) As Dungeon
        Dim maze As Maze(Of Direction) = CreateMaze(mazeColumns, mazeRows)
        Dim locationIds As List(Of Long) = CreateLocations(maze)
        Dim theme = RNG.FromGenerator(themeGenerator)
        PopulateDoors(maze, locationIds)
        PopulateItems(locationIds, difficulty, theme)
        PopulateCreatures(locationIds, difficulty, theme)
        PopulateFeatures(locationIds, difficulty, theme)
        Return CreateDungeon(dungeonName, overworldLocation, locationIds.Select(Function(id) New Location(id)).ToList, difficulty)
    End Function

    Private Shared Sub PopulateFeatures(locationIds As List(Of Long), difficulty As Difficulty, theme As DungeonTheme)
        For Each descriptor In FeatureTypeDescriptors
            Dim spawnCount = RNG.RollDice(descriptor.Value.DungeonSpawnDice(difficulty, theme, locationIds.LongCount))
            While spawnCount > 0
                Dim location = New Location(RNG.FromList(locationIds))
                'TODO: what if the feature type already exists at a given location?
                Feature.Create(location, descriptor.Key)
                spawnCount -= 1
            End While
        Next
    End Sub

    Private Shared Sub PopulateCreatures(locationIds As List(Of Long), difficulty As Difficulty, theme As DungeonTheme)
        Dim locations = locationIds.Select(Function(x) New Location(x))
        For Each characterType In AllCharacterTypes()
            Dim spawnLocations = characterType.SpawnLocations(difficulty, theme, locations)
            For Each spawnLocation In spawnLocations
                Data.CharacterData.Create(characterType.RandomName, characterType, 0, spawnLocation.Id)
            Next
        Next
    End Sub

    Private Shared Sub PopulateItems(locationIds As List(Of Long), difficulty As Difficulty, theme As DungeonTheme)
        Dim locations = locationIds.Select(Function(x) New Location(x))
        For Each itemType In AllItemTypes
            Dim spawnLocations = itemType.SpawnLocations(difficulty, theme, locations)
            For Each spawnLocation In spawnLocations
                spawnLocation.Inventory.Add(Item.Create(itemType))
            Next
        Next
    End Sub

    Private Shared Function CreateDungeon(dungeonName As String, overworldLocation As Location, locations As List(Of Location), difficulty As Difficulty) As Dungeon
        Dim startingLocation = RNG.FromEnumerable(locations.Where(Function(x) x.RouteCount > 1))
        Dim dungeonId =
            DungeonData.Create(dungeonName, overworldLocation.Id, startingLocation.Id, difficulty)
        For Each location In locations
            DungeonLocationData.Write(dungeonId, location.Id)
        Next
        Return New Dungeon(dungeonId)
    End Function

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
                For Each direction In AllCardinalDirections
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
            locationIds.Add(LocationData.Create(LocationType.Dungeon))
        End While
        Return locationIds
    End Function
End Class
