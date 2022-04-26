Imports System.Runtime.CompilerServices

Public Enum ItemType
    None
    LeaveStone
    Food
    Potion
    Dagger
End Enum
Public Module ItemTypeExtensions
    Public ReadOnly AllItemTypes As New List(Of ItemType) From {ItemType.LeaveStone, ItemType.Food, ItemType.Potion, ItemType.Dagger}
    <Extension>
    Function SpawnCount(itemType As ItemType, locationCount As Long) As String
        Select Case itemType
            Case ItemType.LeaveStone
                Return "1d1"
            Case ItemType.Food
                Return $"{locationCount * 3 \ 4}d1"
            Case ItemType.Potion
                Return $"{locationCount \ 3}d1"
            Case ItemType.Dagger
                Return $"{locationCount \ 6}d1"
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
            Case ItemType.Potion
                Return "potion"
            Case ItemType.Dagger
                Return "dagger"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private ReadOnly UsableItemTypes As New HashSet(Of ItemType) From
        {
            ItemType.LeaveStone,
            ItemType.Food,
            ItemType.Potion,
            ItemType.Dagger
        }
    <Extension>
    Function CanUse(itemType As ItemType) As Boolean
        Return UsableItemTypes.Contains(itemType)
    End Function
    <Extension>
    Function UseMessage(itemType As ItemType, characterName As String) As String
        Select Case itemType
            Case ItemType.LeaveStone
                Return $"{characterName} uses the leave stone to leave the dungeon."
            Case ItemType.Food
                Return $"{characterName} eats food."
            Case ItemType.Potion
                Return $"{characterName} drinks a potion."
            Case ItemType.Dagger
                Return $"{characterName} commits seppuku."
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module