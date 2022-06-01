Friend Class PlantFiberDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("plant fiber", False, EquipSlot.None)
        Aliases = New List(Of String) From {"fiber"}
    End Sub
End Class
