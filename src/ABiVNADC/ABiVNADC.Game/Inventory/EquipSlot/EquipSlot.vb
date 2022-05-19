Imports System.Runtime.CompilerServices

Public Enum EquipSlot
    None
    Weapon
    Shield
    Head
    Body
    Legs
    Back
    Neck
End Enum
Public Module EquipSlotExtensions
    Public Function AllEquipSlots() As IEnumerable(Of EquipSlot)
        Return EquipSlotDescriptors.Keys
    End Function
    <Extension>
    Function Name(equipSlot As EquipSlot) As String
        Return EquipSlotDescriptors(equipSlot).Name
    End Function

    Public Function ParseEquipSlot(equipSlotName As String) As EquipSlot
        Return AllEquipSlots.SingleOrDefault(Function(x) x.Name = equipSlotName)
    End Function
End Module
