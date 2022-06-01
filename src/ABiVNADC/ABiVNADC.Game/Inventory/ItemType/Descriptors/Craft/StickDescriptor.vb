Friend Class StickDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("stick", False, EquipSlot.Weapon)
        AttackDice = Function(x) "1d3/2"
    End Sub
    Public Overrides ReadOnly Property CanThrow As Boolean
        Get
            Return True
        End Get
    End Property
End Class
