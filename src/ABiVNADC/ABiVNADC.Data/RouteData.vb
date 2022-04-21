Public Module RouteData
    Friend Const TableName = "Routes"
    Friend Const RouteIdColumn = "RouteId"
    Friend Const FromLocationIdColumn = "FromLocationId"
    Friend Const ToLocationIdColumn = "ToLocationId"
    Friend Const DirectionColumn = "Direction"
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
