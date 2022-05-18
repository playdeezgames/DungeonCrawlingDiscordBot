Public Class EquipSlotDescriptor
    Property Name As String

End Class
Module EquipSlotDescriptorExtensions
    Friend ReadOnly EquipSlotDescriptors As New Dictionary(Of EquipSlot, EquipSlotDescriptor) From
        {
            {
                EquipSlot.Back,
                New EquipSlotDescriptor With
                {
                    .Name = "back"
                }
            },
            {
                EquipSlot.Body,
                New EquipSlotDescriptor With
                {
                    .Name = "body"
                }
            },
            {
                EquipSlot.Head,
                New EquipSlotDescriptor With
                {
                    .Name = "head"
                }
            },
            {
                EquipSlot.Legs,
                New EquipSlotDescriptor With
                {
                    .Name = "legs"
                }
            },
            {
                EquipSlot.Shield,
                New EquipSlotDescriptor With
                {
                    .Name = "shield"
                }
            },
            {
                EquipSlot.Weapon,
                New EquipSlotDescriptor With
                {
                    .Name = "weapon"
                }
            }
        }
End Module