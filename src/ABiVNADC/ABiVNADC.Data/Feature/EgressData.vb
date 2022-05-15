Public Module EgressData
    Friend Const TableName = "Egresses"
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
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, FeatureIdColumn)
    End Function

    Public Function ReadName(featureId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, FeatureIdColumn, featureId, NameColumn)
    End Function
End Module
