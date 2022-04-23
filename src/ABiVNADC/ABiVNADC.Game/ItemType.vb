Imports System.Runtime.CompilerServices

Public Enum ItemType
    LeaveStone
End Enum
Public Module ItemTypeExtensions
    Friend ReadOnly AllItemTypes As New List(Of ItemType) From {ItemType.LeaveStone}
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
End Module