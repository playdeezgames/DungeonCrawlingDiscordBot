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

    Public Function ReadFeatureType(featureId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, FeatureTypeColumn)
    End Function

    Public Function Create(locationId As Long, featureType As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]([{LocationIdColumn}],[{FeatureTypeColumn}]) VALUES(@{LocationIdColumn},@{FeatureTypeColumn});",
            MakeParameter($"@{LocationIdColumn}", locationId),
            MakeParameter($"@{FeatureTypeColumn}", featureType))
        Return LastInsertRowId
    End Function
End Module
