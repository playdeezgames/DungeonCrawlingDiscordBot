Public Class Dungeon
    ReadOnly Property Id As Long
    Sub New(dungeonId As Long)
        Id = dungeonId
    End Sub
    Shared Function FromId(dungeonId As Long) As Dungeon
        Return New Dungeon(dungeonId)
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
    Shared Sub Create(player As Player, dungeonName As String, mazeColumns As Long, mazeRows As Long)
        Dim maze As New Maze(Of Direction)(mazeColumns, mazeRows, DirectionWalker)
        maze.Generate()
        Dim locationIds As New List(Of Long)
        While locationIds.Count < mazeColumns * mazeRows
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
        Dim dungeonId = DungeonData.Create(player.Id, dungeonName, locationIds(0))
        For Each locationId In locationIds
            DungeonLocationData.Write(dungeonId, locationId)
        Next
        For Each itemType In AllItemTypes
            Dim spawnCount = RNG.RollDice(itemType.SpawnCount)
            While spawnCount > 0
                Dim location = New Location(RNG.FromList(locationIds))
                location.Inventory.Add(Item.Create(itemType))
                spawnCount -= 1
            End While
        Next
    End Sub
End Class
