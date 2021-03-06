Public Module DungeonLocationData
    Friend Const TableName = "DungeonLocations"
    Friend Const DungeonIdColumn = DungeonData.DungeonIdColumn
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Sub Initialize()
        DungeonData.Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{DungeonIdColumn}] INT NOT NULL,
                [{LocationIdColumn}] INT NOT NULL UNIQUE,
                FOREIGN KEY ([{DungeonIdColumn}]) REFERENCES [{DungeonData.TableName}]([{DungeonData.DungeonIdColumn}]),
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub

    Public Function ReadForLocation(locationId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, DungeonIdColumn, (LocationIdColumn, locationId))
    End Function

    Public Sub Write(dungeonId As Long, locationId As Long)
        ReplaceRecord(AddressOf Initialize, TableName, (DungeonIdColumn, dungeonId), (LocationIdColumn, locationId))
    End Sub

    Friend Sub ClearForDungeon(dungeonId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, (DungeonIdColumn, dungeonId))
    End Sub
End Module
