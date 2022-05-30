Public Module LandClaimOfficeData
    Friend Const TableName = "LandClaimOffices"
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Const ClaimPriceColumn = "ClaimPrice"
    Friend Sub Initialize()
        FeatureData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{FeatureIdColumn}] INT NOT NULL UNIQUE,
                [{ClaimPriceColumn}] INT NOT NULL,
                FOREIGN KEY ([{FeatureIdColumn}]) REFERENCES [{FeatureData.TableName}]([{FeatureData.FeatureIdColumn}])
            );")
    End Sub

    Public Function Read(featureId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, ClaimPriceColumn, (FeatureIdColumn, featureId))
    End Function

    Public Sub Write(featureId As Long, claimPrice As Long)
        ReplaceRecord(AddressOf Initialize, TableName, FeatureIdColumn, featureId, ClaimPriceColumn, claimPrice)
    End Sub
End Module
