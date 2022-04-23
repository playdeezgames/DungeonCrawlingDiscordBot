Public Class Location
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    ReadOnly Property Routes As Dictionary(Of Direction, Route)
        Get
            Dim result As New Dictionary(Of Direction, Route)
            For Each route In RouteData.ReadForForLocation(Id).Select(Function(id) New Route(id))
                result(route.Direction) = route
            Next
            Return result
        End Get
    End Property

    ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId As Long? = LocationInventoryData.ReadForLocation(Id)
            If Not inventoryId.HasValue Then
                inventoryId = InventoryData.Create()
                LocationInventoryData.Write(Id, inventoryId.Value)
            End If
            Return New Inventory(inventoryId.Value)
        End Get
    End Property
End Class
