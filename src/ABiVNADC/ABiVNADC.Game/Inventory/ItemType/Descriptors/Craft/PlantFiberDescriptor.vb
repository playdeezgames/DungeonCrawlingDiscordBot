Friend Class PlantFiberDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        Aliases = New List(Of String) From {"fiber"}
    End Sub

    Public Overrides Function GetName() As String
        Return "plant fiber"
    End Function
End Class
