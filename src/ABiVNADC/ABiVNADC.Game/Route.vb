Public Class Route
    ReadOnly Property Id As Long
    Sub New(routeId As Long)
        Id = routeId
    End Sub
    ReadOnly Property Direction As Direction
        Get
            Return CType(RouteData.ReadDirection(Id).Value, Direction)
        End Get
    End Property
End Class
