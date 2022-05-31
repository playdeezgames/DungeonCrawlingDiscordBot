Friend Class PlantFiberDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("plant fiber", False)
        Aliases = New List(Of String) From {"fiber"}
    End Sub
End Class
