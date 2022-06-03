Public Module QuestData
    Friend Const TableName = "Quests"
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Const QuestGiverNameColumn = "QuestGiverName"
    Friend Const TargetItemTypeColumn = "TargetItemType"
    Friend Const TargetItemQuantityColumn = "TargetItemQuantity"
    Friend Const RewardItemTypeColumn = "RewardItemType"
    Friend Const RewardItemQuantityColumn = "RewardItemQuantity"

    Public Function ReadForFeatureId(featureId As Long) As Long?
        Dim result = ReadRecordsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, FeatureIdColumn, (FeatureIdColumn, featureId))
        If result.Any Then
            Return result.First
        End If
        Return Nothing
    End Function

    Public Function ReadName(featureId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, QuestGiverNameColumn, (FeatureIdColumn, featureId))
    End Function

    Public Function ReadTargetQuantity(featureId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, TargetItemQuantityColumn, (FeatureIdColumn, featureId))
    End Function

    Public Function ReadRewardQuantity(featureId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, RewardItemQuantityColumn, (FeatureIdColumn, featureId))
    End Function

    Public Function ReadTargetItemType(featureId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, TargetItemTypeColumn, (FeatureIdColumn, featureId))
    End Function

    Friend Sub Initialize()
        FeatureData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{FeatureIdColumn}] INT NOT NULL UNIQUE,
                [{QuestGiverNameColumn}] TEXT NOT NULL,
                [{TargetItemTypeColumn}] INT NOT NULL,
                [{TargetItemQuantityColumn}] INT NOT NULL,
                [{RewardItemTypeColumn}] INT NOT NULL,
                [{RewardItemQuantityColumn}] INT NOT NULL,
                FOREIGN KEY ([{FeatureIdColumn}]) REFERENCES [{FeatureData.TableName}]([{FeatureData.FeatureIdColumn}])
            );")
    End Sub

    Public Sub WriteTargetQuantity(featureId As Long, quantity As Long)
        WriteColumnValue(AddressOf Initialize, TableName, (TargetItemQuantityColumn, quantity), (FeatureIdColumn, featureId))
    End Sub

    Public Function ReadRewardItemType(featureId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, RewardItemTypeColumn, (FeatureIdColumn, featureId))
    End Function

    Public Sub Write(
                    featureId As Long,
                    questGiverName As String,
                    targetItemType As Long,
                    targetQuantity As Long,
                    rewardItemType As Long,
                    rewardQuantity As Long)
        ReplaceRecord(
            AddressOf Initialize,
            TableName,
            (FeatureIdColumn, featureId),
            (QuestGiverNameColumn, questGiverName),
            (TargetItemTypeColumn, targetItemType),
            (TargetItemQuantityColumn, targetQuantity),
            (RewardItemTypeColumn, rewardItemType),
            (RewardItemQuantityColumn, rewardQuantity))
    End Sub
End Module
