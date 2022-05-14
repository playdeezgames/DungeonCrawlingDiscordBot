Public Class QuestGiver
    ReadOnly Property Id As Long
    Sub New(featureId As Long)
        Id = featureId
    End Sub

    Friend Shared Function Create(featureId As Long, name As String, targetItemType As ItemType, targetQuantity As Long, rewardItemType As ItemType, rewardQuantity As Long) As QuestGiver
        QuestData.Write(featureId, name, targetItemType, targetQuantity, rewardItemType, rewardQuantity)
        Return New QuestGiver(featureId)
    End Function

    Friend Shared Function FromId(featureId As Long?) As QuestGiver
        Return If(featureId.HasValue, New QuestGiver(featureId.Value), Nothing)
    End Function

    ReadOnly Property Name As String
        Get
            Return QuestData.ReadName(Id)
        End Get
    End Property
End Class
