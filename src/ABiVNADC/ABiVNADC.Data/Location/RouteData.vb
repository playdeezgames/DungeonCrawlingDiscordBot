Public Module RouteData
    Friend Const TableName = "Routes"
    Friend Const RouteIdColumn = "RouteId"
    Friend Const FromLocationIdColumn = "FromLocationId"
    Friend Const ToLocationIdColumn = "ToLocationId"
    Friend Const DirectionColumn = "Direction"

    Public Function ReadDirection(routeId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, DirectionColumn, (RouteIdColumn, routeId))
    End Function

    Public Function ReadToLocation(routeId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, ToLocationIdColumn, (RouteIdColumn, routeId))
    End Function


    Public Function ReadForForLocation(forLocationId As Long) As List(Of Long)
        Return ReadRecordsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, RouteIdColumn, (FromLocationIdColumn, forLocationId))
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
        Return CreateRecord(
            AddressOf Initialize,
            TableName,
            (FromLocationIdColumn, fromLocationId),
            (DirectionColumn, direction),
            (ToLocationIdColumn, toLocationId))
    End Function
End Module
