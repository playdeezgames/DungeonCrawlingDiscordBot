Friend Class StickDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("stick", False, EquipSlot.Weapon)
        AttackDice = Function(x) "1d3/2"
    End Sub
End Class
