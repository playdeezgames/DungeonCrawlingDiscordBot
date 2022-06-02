Friend Class PlantFiberDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        Aliases = New List(Of String) From {"fiber"}
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "plant fiber"
        End Get
    End Property
End Class
