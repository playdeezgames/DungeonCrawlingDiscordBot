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
        Return ReadRecordsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, FeatureIdColumn, (LocationIdColumn, locationId))
    End Function

    Public Function ReadLocation(featureId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, LocationIdColumn, (FeatureIdColumn, featureId))
    End Function

    Public Function ReadCountForLocation(locationId As Long) As Long
        Return ReadCountForColumnValue(AddressOf Initialize, TableName, (LocationIdColumn, locationId))
    End Function

    Public Sub Clear(featureId As Long)
        CorpseData.Clear(featureId)
        EgressData.Clear(featureId)
        EntranceData.Clear(featureId)
        ClearForColumnValue(AddressOf Initialize, TableName, (FeatureIdColumn, featureId))
    End Sub

    Public Function ReadFeatureType(featureId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, FeatureTypeColumn, (FeatureIdColumn, featureId))
    End Function

    Public Function Create(locationId As Long, featureType As Long) As Long
        ReplaceRecord(AddressOf Initialize, TableName, LocationIdColumn, locationId, FeatureTypeColumn, featureType)
        Return LastInsertRowId
    End Function
End Module
