Public Module FeatureData
    Friend Const TableName = "Features"
    Friend Const FeatureIdColumn = "FeatureId"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const FeatureTypeColumn = "FeatureType"
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{FeatureIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{LocationIdColumn}] INT NOT NULL,
                [{FeatureTypeColumn}] INT NOT NULL,
                UNIQUE([{LocationIdColumn}],[{FeatureTypeColumn}]),
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub

    Public Function ReadForLocation(locationId As Long) As IEnumerable(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, FeatureIdColumn, LocationIdColumn, locationId)
    End Function

    Public Function ReadLocation(featureId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, LocationIdColumn)
    End Function

    Public Function ReadCountForLocation(locationId As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT 
                COUNT(1) 
            FROM [{TableName}] 
            WHERE 
                [{LocationIdColumn}]=@{LocationIdColumn};",
            MakeParameter($"@{LocationIdColumn}", locationId)).Value
    End Function

    Public Function ReadFeatureType(featureId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, FeatureTypeColumn)
    End Function

    Public Function Create(locationId As Long, featureType As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{LocationIdColumn}],[{FeatureTypeColumn}]) VALUES(@{LocationIdColumn},@{FeatureTypeColumn});",
            MakeParameter($"@{LocationIdColumn}", locationId),
            MakeParameter($"@{FeatureTypeColumn}", featureType))
        Return LastInsertRowId
    End Function
End Module
