Imports System.Runtime.CompilerServices

Public Enum ItemType
    None
    LeaveStone
End Enum
Public Module ItemTypeExtensions
    Public ReadOnly AllItemTypes As New List(Of ItemType) From {ItemType.LeaveStone}
    <Extension()>
    Function SpawnCount(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.LeaveStone
                Return "1d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension()>
    Function Name(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.LeaveStone
                Return "leave stone"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension()>
    Function CanUse(itemType As ItemType) As Boolean
        Select Case itemType
            Case ItemType.LeaveStone
                Return True
            Case Else
                Return False
        End Select
    End Function
    <Extension()>
    Function UseMessage(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.LeaveStone
                Return "You use the leave stone to leave the dungeon."
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module