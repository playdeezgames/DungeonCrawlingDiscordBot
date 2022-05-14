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

    ReadOnly Property TargetQuantity As Long
        Get
            Return QuestData.ReadTargetQuantity(Id).Value
        End Get
    End Property

    ReadOnly Property RewardQuantity As Long
        Get
            Return QuestData.ReadRewardQuantity(Id).Value
        End Get
    End Property

    ReadOnly Property TargetItemType As ItemType
        Get
            Return CType(QuestData.ReadTargetItemType(Id).Value, ItemType)
        End Get
    End Property

    ReadOnly Property RewardItemType As ItemType
        Get
            Return CType(QuestData.ReadRewardItemType(Id).Value, ItemType)
        End Get
    End Property

    Friend Sub CompleteQuest(character As Character, items As IEnumerable(Of Item))
        For Each item In items
            item.Destroy()
        Next
        Dim quantity = RewardQuantity
        While quantity > 0
            character.Inventory.Add(Item.Create(RewardItemType))
            quantity -= 1
        End While
        QuestData.WriteTargetQuantity(Id, TargetQuantity + 1)
    End Sub
End Class
