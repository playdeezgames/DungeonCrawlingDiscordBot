Public Module FeatureTextMetadata
    Friend Const TableName = "FeatureTextMetadatas"
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Const FeatureMetadataTypeColumn = "FeatureMetadataType"
    Friend Const MetadataTextColumn = "MetadataText"
    Friend Sub Initialize()
        FeatureData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{FeatureIdColumn}] INT NOT NULL,
                [{FeatureMetadataTypeColumn}] INT NOT NULL,
                [{MetadataTextColumn}] TEXT NOT NULL,
                UNIQUE([{FeatureIdColumn}],[{FeatureMetadataTypeColumn}]),
                FOREIGN KEY ([{FeatureIdColumn}]) REFERENCES [{FeatureData.TableName}]([{FeatureData.FeatureIdColumn}])
            )")
    End Sub
    Public Sub Write(featureId As Long, featureMetadataType As Long, metadataText As String)
        ReplaceRecord(AddressOf Initialize, TableName, FeatureIdColumn, featureId, FeatureMetadataTypeColumn, featureMetadataType, MetadataTextColumn, metadataText)
    End Sub

    Public Function Read(featureId As Long, featureMetadataType As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, FeatureIdColumn, featureId, FeatureMetadataTypeColumn, featureMetadataType, MetadataTextColumn)
    End Function
End Module
