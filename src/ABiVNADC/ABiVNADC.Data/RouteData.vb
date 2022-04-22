Public Module RouteData
    Friend Const TableName = "Routes"
    Friend Const RouteIdColumn = "RouteId"
    Friend Const FromLocationIdColumn = "FromLocationId"
    Friend Const ToLocationIdColumn = "ToLocationId"

    Public Function ReadDirection(routeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, RouteIdColumn, routeId, DirectionColumn)
    End Function

    Public Function ReadToLocation(routeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, RouteIdColumn, routeId, ToLocationIdColumn)
    End Function

    Friend Const DirectionColumn = "Direction"

    Public Function ReadForForLocation(forLocationId As Long) As List(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, RouteIdColumn, FromLocationIdColumn, forLocationId)
    End Function

    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{RouteIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{FromLocationIdColumn}] INT NOT NULL,
                [{DirectionColumn}] INT NOT NULL,
                [{ToLocationIdColumn}] INT NOT NULL,
                UNIQUE([{FromLocationIdColumn}],[{DirectionColumn}]),
                FOREIGN KEY ([{FromLocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY ([{ToLocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub
    Public Function Create(fromLocationId As Long, direction As Long, toLocationId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{FromLocationIdColumn}],
                [{DirectionColumn}],
                [{ToLocationIdColumn}]
            ) 
            VALUES
            (
                @{FromLocationIdColumn},
                @{DirectionColumn},
                @{ToLocationIdColumn}
            );",
            MakeParameter($"@{FromLocationIdColumn}", fromLocationId),
            MakeParameter($"@{DirectionColumn}", direction),
            MakeParameter($"@{ToLocationIdColumn}", toLocationId))
        Return LastInsertRowId
    End Function
End Module
