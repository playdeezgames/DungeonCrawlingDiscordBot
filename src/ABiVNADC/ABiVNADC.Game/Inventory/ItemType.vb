Imports System.Runtime.CompilerServices

Public Enum ItemType
    None
    LeaveStone
    Food
End Enum
Public Module ItemTypeExtensions
    Public ReadOnly AllItemTypes As New List(Of ItemType) From {ItemType.LeaveStone}
    <Extension>
    Function SpawnCount(itemType As ItemType, locationCount As Long) As String
        Select Case itemType
            Case ItemType.LeaveStone
                Return "1d1"
            Case ItemType.Food
                Return $"{locationCount \ 2}d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function Name(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.LeaveStone
                Return "leave stone"
            Case ItemType.Food
                Return "food"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private ReadOnly UsableItemTypes As New HashSet(Of ItemType) From
        {
            ItemType.LeaveStone,
            ItemType.Food
        }
    <Extension>
    Function CanUse(itemType As ItemType) As Boolean
        Return UsableItemTypes.Contains(itemType)
    End Function
    <Extension>
    Function UseMessage(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.LeaveStone
                Return "You use the leave stone to leave the dungeon."
            Case ItemType.Food
                Return "You eat the food."
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module