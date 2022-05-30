Public Module EntranceData
    Friend Const TableName = "Entrances"
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Const NameColumn = "Name"
    Friend Sub Initialize()
        FeatureData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{FeatureIdColumn}] INT NOT NULL UNIQUE,
                [{NameColumn}] TEXT NOT NULL,
                FOREIGN KEY ([{FeatureIdColumn}]) REFERENCES [{FeatureData.TableName}]([{FeatureData.FeatureIdColumn}])
            );")
    End Sub
    Public Function ReadForFeatureId(featureId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, FeatureIdColumn, (FeatureIdColumn, featureId))
    End Function

    Public Function ReadName(featureId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, NameColumn, (FeatureIdColumn, featureId))
    End Function

    Public Sub Write(featureId As Long, name As String)
        ReplaceRecord(AddressOf Initialize, TableName, FeatureIdColumn, featureId, NameColumn, name)
    End Sub

    Friend Sub Clear(featureId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, (FeatureIdColumn, featureId))
    End Sub
End Module
