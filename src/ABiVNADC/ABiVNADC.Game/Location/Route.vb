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
    ReadOnly Property ToLocation As Location
        Get
            Return New Location(RouteData.ReadToLocation(Id).Value)
        End Get
    End Property
End Class
