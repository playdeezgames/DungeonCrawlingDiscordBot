Friend Class ArseholesStatisticDescriptor
    Inherits StatisticTypeDescriptor

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Arseholes"
        End Get
    End Property

    Public Overrides Function Format(current As Long, maximum As Long) As String
        Return $"{current}"
    End Function
End Class
