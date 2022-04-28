Public Enum EquipSlot
    None
    Weapon
    Shield
    Head
    Body
    Legs
End Enum
Public Module EquipSlotExtensions
    Public ReadOnly AllEquipSlots As New List(Of EquipSlot) From
        {
            EquipSlot.Weapon,
            EquipSlot.Shield,
            EquipSlot.Head,
            EquipSlot.Body,
            EquipSlot.Legs
        }
End Module
