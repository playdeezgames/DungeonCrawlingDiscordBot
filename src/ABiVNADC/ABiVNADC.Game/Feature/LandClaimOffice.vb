Public Class LandClaimOffice
    ReadOnly Property Id As Long
    Sub New(featureId As Long)
        Id = featureId
    End Sub
    Property ClaimPrice As Long
        Get
            Return LandClaimOfficeData.Read(Id).Value
        End Get
        Set(value As Long)
            LandClaimOfficeData.Write(Id, value)
        End Set
    End Property

    Friend Shared Function Create(location As Location) As LandClaimOffice
        Dim featureId = FeatureData.Create(location.Id, FeatureType.LandClaimOffice)
        LandClaimOfficeData.Write(featureId, 1)
        Return New LandClaimOffice(featureId)
    End Function
End Class
