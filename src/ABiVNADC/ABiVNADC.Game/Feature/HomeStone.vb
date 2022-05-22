Public Class HomeStone
    ReadOnly Property Id As Long
    Sub New(featureId As Long)
        Id = featureId
    End Sub
    Friend Shared Function Create(location As Location, owner As Character) As HomeStone
        Dim homeStoneId = FeatureData.Create(location.Id, FeatureType.HomeStone)
        FeatureOwnerData.Write(homeStoneId, owner.Id)
        Return New HomeStone(homeStoneId)
    End Function

End Class
