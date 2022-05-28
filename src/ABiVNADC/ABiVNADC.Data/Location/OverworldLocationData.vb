Public Module OverworldLocationData
    Friend Const TableName = "OverworldLocations"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const XColumn = "X"
    Friend Const YColumn = "Y"
    Friend Const TerrainTypeColumn = "TerrainType"

    Public Function ReadTerrainType(locationId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, LocationIdColumn, locationId, TerrainTypeColumn)
    End Function

    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INT NOT NULL UNIQUE,
                [{XColumn}] INT NOT NULL,
                [{YColumn}] INT NOT NULL,
                [{TerrainTypeColumn}] INT NOT NULL,
                UNIQUE([{XColumn}],[{YColumn}]),
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub

    Public Function ReadY(locationId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, LocationIdColumn, locationId, YColumn)
    End Function

    Public Function ReadX(locationId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, LocationIdColumn, locationId, XColumn)
    End Function

    Public Function ReadForXY(x As Long, y As Long) As Long?
        Return ReadColumnValue(Of Long, Long, Long)(AddressOf Initialize, TableName, LocationIdColumn, (XColumn, x), (YColumn, y))
    End Function

    Public Sub Write(locationId As Long, x As Long, y As Long, terrainType As Long)
        ReplaceRecord(AddressOf Initialize, TableName, LocationIdColumn, locationId, XColumn, x, YColumn, y, TerrainTypeColumn, terrainType)
    End Sub
End Module
