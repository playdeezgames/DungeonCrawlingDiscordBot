Public Class Shoppe
    ReadOnly Property Id As Long
    Sub New(shoppeId As Long)
        Id = shoppeId
    End Sub

    Public ReadOnly Property Name As String
        Get
            Return ShoppeData.ReadShoppeName(Id)
        End Get
    End Property

    Public ReadOnly Property InsideLocation As Location
        Get
            Return Location.FromId(ShoppeData.ReadInsideLocation(Id))
        End Get
    End Property

    Public Shared Function FromId(shoppeId As Long?) As Shoppe
        If shoppeId.HasValue Then
            Return New Shoppe(shoppeId.Value)
        End If
        Return Nothing
    End Function
End Class
