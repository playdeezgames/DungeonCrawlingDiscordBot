Public Class Entrance
    ReadOnly Property Id As Long
    Sub New(featureId As Long)
        Id = featureId
    End Sub
    ReadOnly Property Name As String
        Get
            Return EntranceData.ReadName(Id)
        End Get
    End Property

    Friend Shared Function FromId(featureId As Long?) As Entrance
        Return If(featureId.HasValue, New Entrance(featureId.Value), Nothing)
    End Function

    Friend Shared Function Create(fromLocation As Location, name As String) As Entrance
        Dim entranceId = FeatureData.Create(fromLocation.Id, FeatureType.Entrance)
        EntranceData.Write(entranceId, name)
        Return New Entrance(entranceId)
    End Function
End Class
