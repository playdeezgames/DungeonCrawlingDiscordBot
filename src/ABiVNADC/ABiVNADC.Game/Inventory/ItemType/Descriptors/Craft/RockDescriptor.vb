Friend Class RockDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("rock", False, EquipSlot.Weapon)
        AttackDice = Function(x) "1d3/3"
    End Sub
    Public Overrides ReadOnly Property CanThrow As Boolean
        Get
            Return True
        End Get
    End Property
End Class
