Public Enum EquipSlot
    None
    Weapon
End Enum
Public Module EquipSlotExtensions
    Public ReadOnly AllEquipSlots As New List(Of EquipSlot) From {EquipSlot.Weapon}
End Module
