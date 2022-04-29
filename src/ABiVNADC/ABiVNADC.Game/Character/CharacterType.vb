Imports System.Runtime.CompilerServices

Public Enum CharacterType
    None
    N00b
    Goblin
    Orc
    Skeleton
    Zombie
    MinionFish
    BossFish
End Enum
Public Module CharacterTypeExtensions
    Private ReadOnly nameTable As New Dictionary(Of CharacterType, String) From
        {
            {CharacterType.N00b, "n00b"},
            {CharacterType.Goblin, "goblin"},
            {CharacterType.Orc, "orc"},
            {CharacterType.Skeleton, "skeleton"},
            {CharacterType.Zombie, "zombie"},
            {CharacterType.MinionFish, "minion fish"},
            {CharacterType.BossFish, "boss fish"}
        }

    <Extension>
    Function Name(characterType As CharacterType) As String
        Dim result As String = Nothing
        nameTable.TryGetValue(characterType, result)
        Return result
    End Function

    <Extension>
    Function MaximumHealth(characterType As CharacterType, level As Long) As Long
        Select Case characterType
            Case CharacterType.N00b
                Return 5 + level
            Case CharacterType.Goblin
                Return 1 + level
            Case CharacterType.Orc
                Return 1 + level
            Case CharacterType.Skeleton
                Return 1 + level
            Case CharacterType.Zombie
                Return 1 + level
            Case CharacterType.MinionFish
                Return 1 + level
            Case CharacterType.BossFish
                Return 5 + level
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Function MaximumEnergy(characterType As CharacterType, level As Long) As Long
        Select Case characterType
            Case CharacterType.N00b
                Return 7 + level
            Case CharacterType.Goblin
                Return 10 + level
            Case CharacterType.Orc
                Return 8 + level
            Case CharacterType.Skeleton
                Return 1 + level
            Case CharacterType.Zombie
                Return 1 + level
            Case CharacterType.MinionFish
                Return 8 + level
            Case CharacterType.BossFish
                Return 12 + level
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public ReadOnly AllCharacterTypes As New List(Of CharacterType) From
        {
            CharacterType.BossFish,
            CharacterType.MinionFish,
            CharacterType.Zombie,
            CharacterType.Orc,
            CharacterType.Goblin,
            CharacterType.Skeleton,
            CharacterType.N00b
        }
    Private ReadOnly SpawnerTable As New Dictionary(Of Difficulty, Dictionary(Of CharacterType, Func(Of Long, Long))) From
        {
            {
                Difficulty.Yermom,
                New Dictionary(Of CharacterType, Func(Of Long, Long)) From
                {
                    {CharacterType.Goblin, Function(x) x \ 4},
                    {CharacterType.Orc, Function(x) x \ 6},
                    {CharacterType.Skeleton, Function(x) x \ 3},
                    {CharacterType.Zombie, Function(x) x \ 6},
                    {CharacterType.N00b, Function(x) 0},
                    {CharacterType.MinionFish, Function(x) x \ 6},
                    {CharacterType.BossFish, Function(x) 0}
                }
            },
            {
                Difficulty.Easy,
                New Dictionary(Of CharacterType, Func(Of Long, Long)) From
                {
                    {CharacterType.Goblin, Function(x) x \ 3},
                    {CharacterType.Orc, Function(x) x \ 4},
                    {CharacterType.Skeleton, Function(x) x \ 2},
                    {CharacterType.Zombie, Function(x) x \ 4},
                    {CharacterType.N00b, Function(x) 0},
                    {CharacterType.MinionFish, Function(x) x \ 4},
                    {CharacterType.BossFish, Function(x) 0}
                }
            },
            {
                Difficulty.Normal,
                New Dictionary(Of CharacterType, Func(Of Long, Long)) From
                {
                    {CharacterType.Goblin, Function(x) x \ 2},
                    {CharacterType.Orc, Function(x) x \ 3},
                    {CharacterType.Skeleton, Function(x) x * 2 \ 3},
                    {CharacterType.Zombie, Function(x) x \ 3},
                    {CharacterType.N00b, Function(x) 0},
                    {CharacterType.MinionFish, Function(x) x \ 3},
                    {CharacterType.BossFish, Function(x) 1}
                }
            },
            {
                Difficulty.Difficult,
                New Dictionary(Of CharacterType, Func(Of Long, Long)) From
                {
                    {CharacterType.Goblin, Function(x) x * 2 \ 3},
                    {CharacterType.Orc, Function(x) x \ 2},
                    {CharacterType.Skeleton, Function(x) x * 3 \ 4},
                    {CharacterType.Zombie, Function(x) x \ 2},
                    {CharacterType.N00b, Function(x) 0},
                    {CharacterType.MinionFish, Function(x) x \ 2},
                    {CharacterType.BossFish, Function(x) 2}
                }
            },
            {
                Difficulty.Too,
                New Dictionary(Of CharacterType, Func(Of Long, Long)) From
                {
                    {CharacterType.Goblin, Function(x) x * 3 \ 4},
                    {CharacterType.Orc, Function(x) x * 2 \ 3},
                    {CharacterType.Skeleton, Function(x) x},
                    {CharacterType.Zombie, Function(x) x * 2 \ 3},
                    {CharacterType.N00b, Function(x) 0},
                    {CharacterType.MinionFish, Function(x) x * 2 \ 3},
                    {CharacterType.BossFish, Function(x) 4}
                }
            }
        }
    <Extension>
    Public Function SpawnCount(characterType As CharacterType, locationCount As Long, difficulty As Difficulty) As Long
        Return SpawnerTable(difficulty)(characterType)(locationCount)
    End Function

    <Extension>
    Public Function RandomName(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.Goblin
                Return RNG.FromList(Names.Goblin)
            Case CharacterType.Orc
                Return RNG.FromList(Names.Orc)
            Case CharacterType.Skeleton
                Return RNG.FromList(Names.Human)
            Case CharacterType.Zombie
                Return RNG.FromList(Names.Human)
            Case CharacterType.BossFish, CharacterType.MinionFish
                Return RNG.FromList(Names.Fish)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Public Function IsEnemy(characterType As CharacterType) As Boolean
        Select Case characterType
            Case CharacterType.Goblin, CharacterType.Orc, CharacterType.Skeleton, CharacterType.Zombie, CharacterType.MinionFish, CharacterType.BossFish
                Return True
            Case Else
                Return False
        End Select
    End Function

    <Extension>
    Public Function AttackDice(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.N00b
                Return "1d3/3"
            Case CharacterType.Goblin
                Return "1d3/3+1d3/3"
            Case CharacterType.Orc
                Return "1d3/3+1d3/3+1d3/3"
            Case CharacterType.Skeleton
                Return "1d3/3+1d3/3"
            Case CharacterType.Zombie
                Return "1d3/3+1d3/3"
            Case CharacterType.MinionFish
                Return "1d3/3+1d3/3"
            Case CharacterType.BossFish
                Return "1d3/3+1d3/3+1d3/3+1d3/3"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Public Function DefendDice(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.N00b
                Return "1d3/3+1d3/3"
            Case CharacterType.Goblin
                Return "1d6/6"
            Case CharacterType.Orc
                Return "1d6/6+1d6/6"
            Case CharacterType.Skeleton
                Return "1d6/6+1d6/6"
            Case CharacterType.Zombie
                Return "1d6/6+1d6/6+1d6/6"
            Case CharacterType.MinionFish
                Return "1d6/6+1d6/6+1d6/6"
            Case CharacterType.BossFish
                Return "1d6/6+1d6/6+1d6/6+1d6/6+1d6/6"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Public Function FightEnergyCost(characterType As CharacterType) As Long
        Select Case characterType
            Case CharacterType.N00b
                Return 1
            Case CharacterType.Goblin
                Return 1
            Case CharacterType.Orc
                Return 1
            Case CharacterType.MinionFish
                Return 1
            Case CharacterType.BossFish
                Return 1
            Case CharacterType.Skeleton
                Return 0
            Case CharacterType.Zombie
                Return 0
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Public Function CombatRestRoll(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.N00b
                Return "1d1+1d2/2"
            Case CharacterType.Goblin
                Return "1d2/2+1d2/2+1d2/2+1d2/2"
            Case CharacterType.Orc
                Return "1d2/2+1d2/2+1d2/2"
            Case CharacterType.MinionFish
                Return "1d2/2+1d2/2+1d2/2"
            Case CharacterType.BossFish
                Return "2d1+1d2/2+1d2/2+1d2/2"
            Case CharacterType.Skeleton
                Return "0d1"
            Case CharacterType.Zombie
                Return "0d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private ReadOnly LootDropTable As New Dictionary(Of CharacterType, Dictionary(Of ItemType, String)) From
        {
            {
                CharacterType.Goblin,
                New Dictionary(Of ItemType, String) From
                {
                    {ItemType.Food, "1d4/4"},
                    {ItemType.GoblinEar, "1d2/2"}
                }
            },
            {
                CharacterType.Skeleton,
                New Dictionary(Of ItemType, String) From
                {
                    {ItemType.SkullFragment, "1d4/4"}
                }
            },
            {
                CharacterType.Zombie,
                New Dictionary(Of ItemType, String) From
                {
                    {ItemType.ZombieTaint, "1d6/6"}
                }
            },
            {
                CharacterType.MinionFish,
                New Dictionary(Of ItemType, String) From
                {
                    {ItemType.FishScale, "2d2"}
                }
            },
            {
                CharacterType.BossFish,
                New Dictionary(Of ItemType, String) From
                {
                    {ItemType.FishScale, "3d3"},
                    {ItemType.FishFin, "1d2/2"},
                    {ItemType.Jools, "1d1"}
                }
            },
            {
                CharacterType.Orc,
                New Dictionary(Of ItemType, String) From
                {
                    {ItemType.Food, "1d2/2"},
                    {ItemType.OrcTooth, "1d3/3"}
                }
            }
        }
    <Extension>
    Function LootDrops(characterType As CharacterType) As Dictionary(Of ItemType, String)
        Dim drops As New Dictionary(Of ItemType, String)
        If LootDropTable.TryGetValue(characterType, drops) Then
            Return drops
        End If
        Return New Dictionary(Of ItemType, String)
    End Function
    <Extension>
    Function ExperiencePointValue(characterType As CharacterType, level As Long) As Long
        Select Case characterType
            Case CharacterType.N00b
                Return 5
            Case CharacterType.Goblin
                Return 1
            Case CharacterType.Orc
                Return 1
            Case CharacterType.Skeleton
                Return 1
            Case CharacterType.Zombie
                Return 1
            Case CharacterType.MinionFish
                Return 1
            Case CharacterType.BossFish
                Return 5
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function ExperienceGoal(characterType As CharacterType, level As Long) As Long
        Select Case characterType
            Case CharacterType.N00b
                Return 10 * (level + 1)
            Case CharacterType.Goblin
                Return 10 * (level + 1)
            Case CharacterType.Orc
                Return 10 * (level + 1)
            Case CharacterType.Skeleton
                Return 10 * (level + 1)
            Case CharacterType.Zombie
                Return 10 * (level + 1)
            Case CharacterType.MinionFish
                Return 10 * (level + 1)
            Case CharacterType.BossFish
                Return 10 * (level + 1)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module