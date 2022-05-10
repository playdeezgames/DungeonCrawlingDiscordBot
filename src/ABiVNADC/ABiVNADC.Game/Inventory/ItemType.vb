Imports System.Runtime.CompilerServices

Public Enum ItemType
    None
    Food
    Potion
    Dagger
    ShortSword
    LongSword
    Trousers
    Helmet
    Shield
    ChainMail
    PlateMail
    GoblinEar
    OrcTooth
    SkullFragment
    ZombieTaint
    FishScale
    FishFin
    Jools
End Enum
Public Module ItemTypeExtensions
    Public ReadOnly AllItemTypes As New List(Of ItemType) From
        {
            ItemType.Food,
            ItemType.Potion,
            ItemType.Dagger,
            ItemType.ShortSword,
            ItemType.LongSword,
            ItemType.Shield,
            ItemType.ChainMail,
            ItemType.PlateMail,
            ItemType.Helmet,
            ItemType.Trousers,
            ItemType.GoblinEar,
            ItemType.OrcTooth,
            ItemType.SkullFragment,
            ItemType.ZombieTaint,
            ItemType.FishFin,
            ItemType.FishScale,
            ItemType.Jools
        }
    Private ReadOnly SpawnerTable As New Dictionary(Of Difficulty, Dictionary(Of ItemType, Func(Of Long, String))) From
        {
            {
                Difficulty.Yermom,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.Food, Function(locationCount) $"{locationCount * 3 \ 2}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount * 2 \ 3}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 2}d1"},
                    {ItemType.ShortSword, Function(locationCount) $"{locationCount \ 3}d1"},
                    {ItemType.LongSword, Function(locationCount) $"{locationCount \ 6}d1"},
                    {ItemType.Shield, Function(locationCount) $"4d2/2"},
                    {ItemType.Helmet, Function(locationCount) $"4d2/2"},
                    {ItemType.ChainMail, Function(locationCount) $"2d2/2"},
                    {ItemType.PlateMail, Function(locationCount) $"1d2/2"},
                    {ItemType.Trousers, Function(locationCount) $"1d1"},
                    {ItemType.GoblinEar, Function(x) "0d1"},
                    {ItemType.OrcTooth, Function(x) "0d1"},
                    {ItemType.SkullFragment, Function(x) "0d1"},
                    {ItemType.ZombieTaint, Function(x) "0d1"},
                    {ItemType.FishScale, Function(x) "0d1"},
                    {ItemType.FishFin, Function(x) "0d1"},
                    {ItemType.Jools, Function(x) "0d1"}
                }
            },
            {
                Difficulty.Easy,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.Food, Function(locationCount) $"{locationCount}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount \ 2}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 4}d1"},
                    {ItemType.ShortSword, Function(locationCount) $"{locationCount \ 6}d1"},
                    {ItemType.LongSword, Function(locationCount) $"{locationCount \ 8}d1"},
                    {ItemType.Shield, Function(locationCount) $"4d2/2"},
                    {ItemType.Helmet, Function(locationCount) $"4d2/2"},
                    {ItemType.ChainMail, Function(locationCount) $"2d2/2"},
                    {ItemType.PlateMail, Function(locationCount) $"1d2/2"},
                    {ItemType.Trousers, Function(locationCount) $"1d1"},
                    {ItemType.GoblinEar, Function(x) "0d1"},
                    {ItemType.OrcTooth, Function(x) "0d1"},
                    {ItemType.SkullFragment, Function(x) "0d1"},
                    {ItemType.ZombieTaint, Function(x) "0d1"},
                    {ItemType.FishScale, Function(x) "0d1"},
                    {ItemType.FishFin, Function(x) "0d1"},
                    {ItemType.Jools, Function(x) "0d1"}
                }
            },
            {
                Difficulty.Normal,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.Food, Function(locationCount) $"{locationCount * 3 \ 4}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount \ 3}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 6}d1"},
                    {ItemType.ShortSword, Function(locationCount) $"{locationCount \ 8}d1"},
                    {ItemType.LongSword, Function(locationCount) $"{locationCount \ 12}d1"},
                    {ItemType.Shield, Function(locationCount) $"4d2/2"},
                    {ItemType.Helmet, Function(locationCount) $"4d2/2"},
                    {ItemType.ChainMail, Function(locationCount) $"2d2/2"},
                    {ItemType.PlateMail, Function(locationCount) $"1d2/2"},
                    {ItemType.Trousers, Function(locationCount) $"1d1"},
                    {ItemType.GoblinEar, Function(x) "0d1"},
                    {ItemType.OrcTooth, Function(x) "0d1"},
                    {ItemType.SkullFragment, Function(x) "0d1"},
                    {ItemType.ZombieTaint, Function(x) "0d1"},
                    {ItemType.FishScale, Function(x) "0d1"},
                    {ItemType.FishFin, Function(x) "0d1"},
                    {ItemType.Jools, Function(x) "0d1"}
                }
            },
            {
                Difficulty.Difficult,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.Food, Function(locationCount) $"{locationCount * 2 \ 3}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount \ 4}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 8}d1"},
                    {ItemType.ShortSword, Function(locationCount) $"{locationCount \ 12}d1"},
                    {ItemType.LongSword, Function(locationCount) $"{locationCount \ 6}d2/2"},
                    {ItemType.Shield, Function(locationCount) $"4d2/2"},
                    {ItemType.Helmet, Function(locationCount) $"4d2/2"},
                    {ItemType.ChainMail, Function(locationCount) $"2d2/2"},
                    {ItemType.PlateMail, Function(locationCount) $"1d2/2"},
                    {ItemType.Trousers, Function(locationCount) $"1d1"},
                    {ItemType.GoblinEar, Function(x) "0d1"},
                    {ItemType.OrcTooth, Function(x) "0d1"},
                    {ItemType.SkullFragment, Function(x) "0d1"},
                    {ItemType.ZombieTaint, Function(x) "0d1"},
                    {ItemType.FishScale, Function(x) "0d1"},
                    {ItemType.FishFin, Function(x) "0d1"},
                    {ItemType.Jools, Function(x) "0d1"}
                }
            },
            {
                Difficulty.Too,
                New Dictionary(Of ItemType, Func(Of Long, String)) From
                {
                    {ItemType.Food, Function(locationCount) $"{locationCount * 1 \ 2}d1"},
                    {ItemType.Potion, Function(locationCount) $"{locationCount \ 6}d1"},
                    {ItemType.Dagger, Function(locationCount) $"{locationCount \ 12}d1"},
                    {ItemType.ShortSword, Function(locationCount) $"{locationCount \ 6}d2/2"},
                    {ItemType.LongSword, Function(locationCount) $"{locationCount \ 8}d2/2"},
                    {ItemType.Shield, Function(locationCount) $"4d2/2"},
                    {ItemType.Helmet, Function(locationCount) $"4d2/2"},
                    {ItemType.ChainMail, Function(locationCount) $"2d2/2"},
                    {ItemType.PlateMail, Function(locationCount) $"1d2/2"},
                    {ItemType.Trousers, Function(locationCount) $"1d1"},
                    {ItemType.GoblinEar, Function(x) "0d1"},
                    {ItemType.OrcTooth, Function(x) "0d1"},
                    {ItemType.SkullFragment, Function(x) "0d1"},
                    {ItemType.ZombieTaint, Function(x) "0d1"},
                    {ItemType.FishScale, Function(x) "0d1"},
                    {ItemType.FishFin, Function(x) "0d1"},
                    {ItemType.Jools, Function(x) "0d1"}
                }
            }
        }

    <Extension>
    Function SpawnCount(itemType As ItemType, locationCount As Long, difficulty As Difficulty) As String
        Return SpawnerTable(difficulty)(itemType)(locationCount)
    End Function

    <Extension>
    Function Name(itemType As ItemType) As String
        Return ItemTypeDescriptors(itemType).Name
    End Function
    Private ReadOnly UsableItemTypes As New HashSet(Of ItemType) From
        {
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
            Case ItemType.Dagger, ItemType.ShortSword, ItemType.LongSword
                Return EquipSlot.Weapon
            Case ItemType.Trousers
                Return EquipSlot.Legs
            Case ItemType.Helmet
                Return EquipSlot.Head
            Case ItemType.ChainMail, ItemType.PlateMail
                Return EquipSlot.Body
            Case ItemType.Shield
                Return EquipSlot.Shield
            Case Else
                Return EquipSlot.None
        End Select
    End Function

    <Extension>
    Function AttackDice(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.Dagger
                Return "1d2/2"
            Case ItemType.ShortSword
                Return "1d2/2+1d2/2"
            Case ItemType.LongSword
                Return "1d2/2+1d2/2+1d2/2"
            Case Else
                Return "0d1"
        End Select
    End Function

    <Extension>
    Function HasAttackDice(itemType As ItemType) As Boolean
        Return itemType.AttackDice <> "0d1"
    End Function


    <Extension>
    Function DefendDice(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.Helmet
                Return "1d3/3"
            Case ItemType.Shield
                Return "1d3/3"
            Case ItemType.ChainMail
                Return "1d3/3"
            Case ItemType.PlateMail
                Return "1d3/3+1d3/3"
            Case Else
                Return "0d1"
        End Select
    End Function

    <Extension>
    Function HasDefendDice(itemType As ItemType) As Boolean
        Return itemType.DefendDice <> "0d1"
    End Function

    <Extension>
    Function HasArmorDurability(itemType As ItemType) As Boolean
        Return itemType.ArmorDurability > 0
    End Function

    <Extension>
    Function ArmorDurability(itemType As ItemType) As Long
        Select Case itemType
            Case ItemType.ChainMail
                Return 20
            Case ItemType.Helmet
                Return 5
            Case ItemType.PlateMail
                Return 35
            Case ItemType.Trousers
                Return 1
            Case ItemType.Shield
                Return 10
            Case Else
                Return 0
        End Select
    End Function

    <Extension>
    Function HasWeaponDurability(itemType As ItemType) As Boolean
        Return itemType.WeaponDurability > 0
    End Function

    <Extension>
    Function WeaponDurability(itemType As ItemType) As Long
        Select Case itemType
            Case ItemType.Dagger
                Return 5
            Case ItemType.ShortSword
                Return 10
            Case ItemType.LongSword
                Return 20
            Case Else
                Return 0
        End Select
    End Function

    <Extension>
    Function CanBuyGenerator(itemType As ItemType) As Dictionary(Of Boolean, Integer)
        Select Case itemType
            Case ItemType.Food
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.Potion
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.Dagger
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.ShortSword
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.LongSword
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.Trousers
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.Helmet
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.Shield
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.ChainMail
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.PlateMail
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case ItemType.Jools
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}, {True, 1}}
            Case Else
                Return New Dictionary(Of Boolean, Integer) From {{False, 1}}
        End Select
    End Function

    <Extension>
    Function BuyPriceDice(itemType As ItemType) As String
        Return "0d1"
    End Function

    <Extension>
    Function CanSellGenerator(itemType As ItemType) As Dictionary(Of Boolean, Integer)
        Return New Dictionary(Of Boolean, Integer) From {{False, 1}}
    End Function

    <Extension>
    Function SellPriceDice(itemType As ItemType) As String
        Return "0d1"
    End Function
End Module