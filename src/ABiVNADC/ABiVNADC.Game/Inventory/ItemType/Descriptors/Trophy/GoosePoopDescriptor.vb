Friend Class GoosePoopDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(EquipSlot.Weapon)
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"poop"}
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "goose poop"
    End Function
End Class
