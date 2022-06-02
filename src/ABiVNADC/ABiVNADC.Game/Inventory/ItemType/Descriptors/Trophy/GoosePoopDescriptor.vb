Friend Class GoosePoopDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"poop"}
        IsTrophy = True
    End Sub
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Weapon
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "goose poop"
        End Get
    End Property
End Class
