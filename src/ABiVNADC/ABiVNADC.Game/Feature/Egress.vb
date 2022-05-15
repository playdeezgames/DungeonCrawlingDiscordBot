Public Class Egress
    ReadOnly Property Id As Long

    Sub New(featureId As Long)
        Id = featureId
    End Sub

    ReadOnly Property Name As String
        Get
            Return EgressData.ReadName(Id)
        End Get
    End Property

    Friend Shared Function FromId(featureId As Long?) As Egress
        Return If(featureId.HasValue, New Egress(featureId.Value), Nothing)
    End Function

    Friend Shared Function Create(toLocation As Location, name As String) As Egress
        Dim egressId = FeatureData.Create(toLocation.Id, FeatureType.Egress)
        EgressData.Write(egressId, name)
        Return New Egress(egressId)
    End Function
End Class
