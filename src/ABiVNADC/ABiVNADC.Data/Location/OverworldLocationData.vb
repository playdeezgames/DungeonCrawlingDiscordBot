Public Module OverworldLocationData
    Friend Const TableName = "OverworldLocations"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const XColumn = "X"
    Friend Const YColumn = "Y"
    Friend Const TerrainTypeColumn = "TerrainType"
    Friend Const PerilThresholdColumn = "PerilThreshold"
    Friend Const PerilColumn = "Peril"
    Friend Const ForageDepletionColumn = "ForageDepletion"

    Public Function ReadPeril(locationId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, PerilColumn, (LocationIdColumn, locationId))
    End Function

    Public Function ReadPerilThreshold(locationId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, PerilThresholdColumn, (LocationIdColumn, locationId))
    End Function

    Public Sub WritePeril(locationId As Long, peril As Long)
        WriteColumnValue(AddressOf Initialize, TableName, (LocationIdColumn, locationId), (PerilColumn, peril))
    End Sub

    Public Function ReadTerrainType(locationId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, TerrainTypeColumn, (LocationIdColumn, locationId))
    End Function

    Public Function ReadForageDepletion(locationId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, ForageDepletionColumn, (LocationIdColumn, locationId))
    End Function

    Public Sub WriteForageDeplection(locationId As Long, depletion As Long)
        WriteColumnValue(AddressOf Initialize, TableName, (LocationIdColumn, locationId), (ForageDepletionColumn, depletion))
    End Sub

    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INT NOT NULL UNIQUE,
                [{XColumn}] INT NOT NULL,
                [{YColumn}] INT NOT NULL,
                [{TerrainTypeColumn}] INT NOT NULL,
                [{PerilColumn}] INT NOT NULL,
                [{PerilThresholdColumn}] INT NOT NULL,
                [{ForageDepletionColumn}] INT NOT NULL,
                UNIQUE([{XColumn}],[{YColumn}]),
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub

    Public Function ReadY(locationId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, YColumn, (LocationIdColumn, locationId))
    End Function

    Public Function ReadX(locationId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, XColumn, (LocationIdColumn, locationId))
    End Function

    Public Function ReadForXY(x As Long, y As Long) As Long?
        Return ReadColumnValue(Of Long, Long, Long)(AddressOf Initialize, TableName, LocationIdColumn, (XColumn, x), (YColumn, y))
    End Function

    Public Sub Write(locationId As Long, x As Long, y As Long, terrainType As Long, peril As Long, perilThreshold As Long, forageDepletion As Long)
        ReplaceRecord(
            AddressOf Initialize,
            TableName,
            (LocationIdColumn, locationId),
            (XColumn, x),
            (YColumn, y),
            (TerrainTypeColumn, terrainType),
            (PerilColumn, peril),
            (PerilThresholdColumn, perilThreshold),
            (ForageDepletionColumn, forageDepletion))
    End Sub
End Module
