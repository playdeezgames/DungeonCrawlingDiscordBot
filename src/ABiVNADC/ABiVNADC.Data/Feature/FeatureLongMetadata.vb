Public Module FeatureLongMetadata
    Friend Const TableName = "FeatureLongMetadatas"
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Const FeatureMetadataTypeColumn = "FeatureMetadataType"
    Friend Const MetadataLongColumn = "MetadataLong"
    Friend Sub Initialize()
        FeatureData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{FeatureIdColumn}] INT NOT NULL,
                [{FeatureMetadataTypeColumn}] INT NOT NULL,
                [{MetadataLongColumn}] TEXT NOT NULL,
                UNIQUE([{FeatureIdColumn}],[{FeatureMetadataTypeColumn}]),
                FOREIGN KEY ([{FeatureIdColumn}]) REFERENCES [{FeatureData.TableName}]([{FeatureData.FeatureIdColumn}])
            )")
    End Sub
    Public Sub Write(featureId As Long, featureMetadataType As Long, metadataLong As Long)
        ReplaceRecord(AddressOf Initialize, TableName, FeatureIdColumn, featureId, FeatureMetadataTypeColumn, featureMetadataType, MetadataLongColumn, metadataLong)
    End Sub

    Public Function Read(featureId As Long, featureMetadataType As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{MetadataLongColumn}] FROM [{TableName}] WHERE [{FeatureIdColumn}]=@{FeatureIdColumn} AND [{FeatureMetadataTypeColumn}]=@{FeatureMetadataTypeColumn};",
            MakeParameter($"@{FeatureIdColumn}", featureId),
            MakeParameter($"@{FeatureMetadataTypeColumn}", featureMetadataType))
    End Function
End Module
