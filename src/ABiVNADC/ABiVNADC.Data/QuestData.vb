Public Module QuestData
    Friend Const TableName = "Quests"
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Const QuestGiverNameColumn = "QuestGiverName"
    Friend Const TargetItemTypeColumn = "TargetItemType"
    Friend Const TargetItemQuantityColumn = "TargetItemQuantity"
    Friend Const RewardItemTypeColumn = "RewardItemType"
    Friend Const RewardItemQuantityColumn = "RewardItemQuantity"

    Public Function ReadForFeatureId(featureId As Long) As Long?
        Dim result = ReadIdsWithColumnValue(AddressOf Initialize, TableName, FeatureIdColumn, FeatureIdColumn, featureId)
        If result.Any Then
            Return result.First
        End If
        Return Nothing
    End Function

    Public Function ReadName(featureId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, FeatureIdColumn, featureId, QuestGiverNameColumn)
    End Function

    Public Function ReadTargetQuantity(featureId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, TargetItemQuantityColumn)
    End Function

    Public Function ReadRewardQuantity(featureId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, RewardItemQuantityColumn)
    End Function

    Public Function ReadTargetItemType(featureId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, TargetItemTypeColumn)
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

    Public Function ReadRewardItemType(featureId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, RewardItemTypeColumn)
    End Function

    Public Sub Write(
                    featureId As Long,
                    questGiverName As String,
                    targetItemType As Long,
                    targetQuantity As Long,
                    rewardItemType As Long,
                    rewardQuantity As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]
            (
                [{FeatureIdColumn}],
                [{QuestGiverNameColumn}],
                [{TargetItemTypeColumn}],
                [{TargetItemQuantityColumn}],
                [{RewardItemTypeColumn}],
                [{RewardItemQuantityColumn}]
            ) 
            VALUES 
            (
                @{FeatureIdColumn},
                @{QuestGiverNameColumn},
                @{TargetItemTypeColumn},
                @{TargetItemQuantityColumn},
                @{RewardItemTypeColumn},
                @{RewardItemQuantityColumn});",
            MakeParameter($"@{FeatureIdColumn}", featureId),
            MakeParameter($"@{QuestGiverNameColumn}", questGiverName),
            MakeParameter($"@{TargetItemTypeColumn}", targetItemType),
            MakeParameter($"@{TargetItemQuantityColumn}", targetQuantity),
            MakeParameter($"@{RewardItemTypeColumn}", rewardItemType),
            MakeParameter($"@{RewardItemQuantityColumn}", rewardQuantity))
    End Sub
End Module
