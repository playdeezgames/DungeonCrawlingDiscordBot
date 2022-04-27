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
    Private ReadOnly SpawnerTable As New Dictionary(Of Difficulty, Dictionary(Of ItemType, Func(Of Long, String))) From
        {
            {
                Difficulty.Yermom,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.LeaveStone, Function(locationCount) "1d1"},
                    {ItemType.Food, Function(locationCount) $"{locationCount * 3 \ 2}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount * 2 \ 3}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 2}d1"}
                }
            },
            {
                Difficulty.Easy,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.LeaveStone, Function(locationCount) "1d1"},
                    {ItemType.Food, Function(locationCount) $"{locationCount}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount \ 2}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 4}d1"}
                }
            },
            {
                Difficulty.Normal,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.LeaveStone, Function(locationCount) "1d1"},
                    {ItemType.Food, Function(locationCount) $"{locationCount * 3 \ 4}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount \ 3}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 6}d1"}
                }
            },
            {
                Difficulty.Difficult,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.LeaveStone, Function(locationCount) "1d1"},
                    {ItemType.Food, Function(locationCount) $"{locationCount * 2 \ 3}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount \ 4}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 8}d1"}
                }
            },
            {
                Difficulty.Too,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.LeaveStone, Function(locationCount) "1d1"},
                    {ItemType.Food, Function(locationCount) $"{locationCount * 1 \ 2}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount \ 6}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 12}d1"}
                }
            }
        }

    <Extension>
    Function SpawnCount(itemType As ItemType, locationCount As Long, difficulty As Difficulty) As String
        Return SpawnerTable(difficulty)(itemType)(locationCount)
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
    Function CanEquip(itemType As ItemType) As Boolean
        Return itemType.EquipSlot <> Game.EquipSlot.None
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
    Public Function ParseItemType(itemTypeName As String) As ItemType
        Return AllItemTypes.SingleOrDefault(Function(x) x.Name = itemTypeName)
    End Function

    <Extension>
    Function EquipSlot(itemType As ItemType) As EquipSlot
        Select Case itemType
            Case ItemType.Dagger
                Return EquipSlot.Weapon
            Case Else
                Return EquipSlot.None
        End Select
    End Function

    <Extension>
    Function AttackDice(itemType As ItemType) As String
        Select Case itemType
            Case Else
                Return "0d1"
        End Select
    End Function

    <Extension>
    Function HasAttackDice(itemType As ItemType) As Boolean
        Return itemType.AttackDice <> "0d1"
    End Function
End Module