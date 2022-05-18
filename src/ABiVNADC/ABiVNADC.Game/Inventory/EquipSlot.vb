Imports System.Runtime.CompilerServices

Public Enum EquipSlot
    None
    Weapon
    Shield
    Head
    Body
    Legs
    Back
End Enum
Public Module EquipSlotExtensions
    Public ReadOnly AllEquipSlots As New List(Of EquipSlot) From
        {
            EquipSlot.Weapon,
            EquipSlot.Shield,
            EquipSlot.Head,
            EquipSlot.Body,
            EquipSlot.Legs,
            EquipSlot.Back
        }
    <Extension>
    Function Name(equipSlot As EquipSlot) As String
        Select Case equipSlot
            Case EquipSlot.Body
                Return "body"
            Case EquipSlot.Head
                Return "head"
            Case EquipSlot.Legs
                Return "legs"
            Case EquipSlot.Shield
                Return "shield"
            Case EquipSlot.Weapon
                Return "weapon"
            Case EquipSlot.Back
                Return "back"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function ParseEquipSlot(equipSlotName As String) As EquipSlot
        Return AllEquipSlots.SingleOrDefault(Function(x) x.Name = equipSlotName)
    End Function
End Module
