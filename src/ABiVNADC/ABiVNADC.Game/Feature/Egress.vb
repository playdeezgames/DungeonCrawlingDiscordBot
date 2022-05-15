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
End Class
