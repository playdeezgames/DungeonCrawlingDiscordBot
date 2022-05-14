Public Module CorpseData
    Friend Const TableName = "Corpses"
    Friend Const CorpseIdColumn = "CorpseId"
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Const CorpseNameColumn = "CorpseName"

    Public Function ReadName(corpseId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CorpseIdColumn, corpseId, CorpseNameColumn)
    End Function

    Friend Sub Initialize()
        FeatureData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CorpseIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{FeatureIdColumn}] INT NOT NULL,
                [{CorpseNameColumn}] TEXT NOT NULL,
                FOREIGN KEY([{FeatureIdColumn}]) REFERENCES [{FeatureData.TableName}]([{FeatureData.FeatureIdColumn}])
            );")
    End Sub

    Public Function ReadForFeature(featureId As Long) As IEnumerable(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, CorpseIdColumn, FeatureIdColumn, featureId)
    End Function

    Public Function Create(featureId As Long, corpseName As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{FeatureIdColumn}],
                [{CorpseNameColumn}]
            ) 
            VALUES
            (
                @{FeatureIdColumn},
                @{CorpseNameColumn}
            );",
            MakeParameter($"@{FeatureIdColumn}", featureId),
            MakeParameter($"@{CorpseNameColumn}", corpseName))
        Return LastInsertRowId
    End Function
End Module
