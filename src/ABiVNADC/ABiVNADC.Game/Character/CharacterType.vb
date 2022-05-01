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
Friend Structure CharacterTypeDescriptor
    Friend Name As String
    Friend MaximumHealth As Func(Of Long, Long)
    Friend MaximumEnergy As Func(Of Long, Long)
    Friend NameTable As List(Of String)
    Friend IsEnemy As Boolean
    Friend AttackDice As String
    Friend DefendDice As String
    Friend FightEnergyCost As Long
    Friend CombatRestRoll As String
    Friend SpawnCount As Func(Of Difficulty, Func(Of Long, Long))
    Friend LootDrops As Dictionary(Of ItemType, String)
    Friend ExperiencePointValue As Long
    Friend ExperiencePointGoal As Func(Of Long, Long)
    Friend ValidBribes As HashSet(Of ItemType)
End Structure
Public Module CharacterTypeExtensions
    Private ReadOnly Descriptors As New Dictionary(Of CharacterType, CharacterTypeDescriptor) From
        {
            {
                CharacterType.N00b,
                New CharacterTypeDescriptor With
                {
                    .Name = "n00b",
                    .MaximumHealth = Function(l) l + 5,
                    .MaximumEnergy = Function(l) l + 7,
                    .NameTable = Names.Human,
                    .IsEnemy = False,
                    .AttackDice = "1d3/3",
                    .DefendDice = "1d3/3+1d3/3",
                    .FightEnergyCost = 1,
                    .CombatRestRoll = "1d1+1d2/2",
                    .SpawnCount = Function(difficulty) Function(x) 0,
                    .LootDrops = New Dictionary(Of ItemType, String),
                    .ExperiencePointValue = 5,
                    .ExperiencePointGoal = Function(x) 10 * (x + 1),
                    .ValidBribes = New HashSet(Of ItemType)
                }
            },
            {
                CharacterType.Goblin,
                New CharacterTypeDescriptor With
                {
                    .Name = "goblin",
                    .MaximumHealth = Function(l) l + 1,
                    .MaximumEnergy = Function(l) l + 10,
                    .NameTable = Names.Goblin,
                    .IsEnemy = True,
                    .AttackDice = "1d3/3+1d3/3",
                    .DefendDice = "1d6/6",
                    .FightEnergyCost = 1,
                    .CombatRestRoll = "1d2/2+1d2/2+1d2/2+1d2/2",
                    .SpawnCount = Function(difficulty)
                                      Select Case difficulty
                                          Case Difficulty.Yermom
                                              Return Function(x) x * 1 \ 4
                                          Case Difficulty.Easy
                                              Return Function(x) x * 1 \ 3
                                          Case Difficulty.Normal
                                              Return Function(x) x * 1 \ 2
                                          Case Difficulty.Difficult
                                              Return Function(x) x * 2 \ 3
                                          Case Difficulty.Too
                                              Return Function(x) x * 3 \ 4
                                          Case Else
                                              Throw New NotImplementedException
                                      End Select
                                  End Function,
                    .LootDrops = New Dictionary(Of ItemType, String) From
                    {
                        {ItemType.Food, "1d4/4"},
                        {ItemType.GoblinEar, "1d2/2"}
                    },
                    .ExperiencePointValue = 1,
                    .ExperiencePointGoal = Function(x) 10 * (x + 1),
                    .ValidBribes = New HashSet(Of ItemType) From {ItemType.Food, ItemType.Potion, ItemType.Jools}
                }
            },
            {
                CharacterType.Orc,
                New CharacterTypeDescriptor With
                {
                    .Name = "orc",
                    .MaximumHealth = Function(l) l + 1,
                    .MaximumEnergy = Function(l) l + 8,
                    .NameTable = Names.Orc,
                    .IsEnemy = True,
                    .AttackDice = "1d3/3+1d3/3+1d3/3",
                    .DefendDice = "1d6/6+1d6/6",
                    .FightEnergyCost = 1,
                    .CombatRestRoll = "1d2/2+1d2/2+1d2/2",
                    .SpawnCount = Function(difficulty)
                                      Select Case difficulty
                                          Case Difficulty.Yermom
                                              Return Function(x) x * 1 \ 6
                                          Case Difficulty.Easy
                                              Return Function(x) x * 1 \ 4
                                          Case Difficulty.Normal
                                              Return Function(x) x * 1 \ 3
                                          Case Difficulty.Difficult
                                              Return Function(x) x * 1 \ 2
                                          Case Difficulty.Too
                                              Return Function(x) x * 2 \ 3
                                          Case Else
                                              Throw New NotImplementedException
                                      End Select
                                  End Function,
                    .LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.Food, "1d2/2"},
                            {ItemType.OrcTooth, "1d3/3"}
                        },
                    .ExperiencePointValue = 1,
                    .ExperiencePointGoal = Function(x) 10 * (x + 1),
                    .ValidBribes = New HashSet(Of ItemType) From {ItemType.Food, ItemType.Potion, ItemType.Jools}
                }
            },
            {
                CharacterType.Skeleton,
                New CharacterTypeDescriptor With
                {
                    .Name = "skeleton",
                    .MaximumHealth = Function(l) l + 1,
                    .MaximumEnergy = Function(l) 1,
                    .NameTable = Names.Human,
                    .IsEnemy = True,
                    .AttackDice = "1d3/3+1d3/3",
                    .DefendDice = "1d6/6+1d6/6",
                    .FightEnergyCost = 0,
                    .CombatRestRoll = "0d1",
                    .SpawnCount = Function(difficulty)
                                      Select Case difficulty
                                          Case Difficulty.Yermom
                                              Return Function(x) x * 1 \ 3
                                          Case Difficulty.Easy
                                              Return Function(x) x * 1 \ 2
                                          Case Difficulty.Normal
                                              Return Function(x) x * 2 \ 3
                                          Case Difficulty.Difficult
                                              Return Function(x) x * 3 \ 4
                                          Case Difficulty.Too
                                              Return Function(x) x
                                          Case Else
                                              Throw New NotImplementedException
                                      End Select
                                  End Function,
                    .LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.SkullFragment, "1d4/4"}
                        },
                    .ExperiencePointValue = 1,
                    .ExperiencePointGoal = Function(x) 10 * (x + 1),
                    .ValidBribes = New HashSet(Of ItemType)
                }
            },
            {
                CharacterType.Zombie,
                New CharacterTypeDescriptor With
                {
                    .Name = "zombie",
                    .MaximumHealth = Function(l) l + 1,
                    .MaximumEnergy = Function(l) 1,
                    .NameTable = Names.Human,
                    .IsEnemy = True,
                    .AttackDice = "1d3/3+1d3/3",
                    .DefendDice = "1d6/6+1d6/6+1d6/6",
                    .FightEnergyCost = 0,
                    .CombatRestRoll = "0d1",
                    .SpawnCount = Function(difficulty)
                                      Select Case difficulty
                                          Case Difficulty.Yermom
                                              Return Function(x) x * 1 \ 6
                                          Case Difficulty.Easy
                                              Return Function(x) x * 1 \ 4
                                          Case Difficulty.Normal
                                              Return Function(x) x * 1 \ 3
                                          Case Difficulty.Difficult
                                              Return Function(x) x * 1 \ 2
                                          Case Difficulty.Too
                                              Return Function(x) x * 2 \ 3
                                          Case Else
                                              Throw New NotImplementedException
                                      End Select
                                  End Function,
                    .LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.ZombieTaint, "1d6/6"}
                        },
                    .ExperiencePointValue = 1,
                    .ExperiencePointGoal = Function(x) 10 * (x + 1),
                    .ValidBribes = New HashSet(Of ItemType)
                }
            },
            {
                CharacterType.MinionFish,
                New CharacterTypeDescriptor With
                {
                    .Name = "minion fish",
                    .MaximumHealth = Function(l) l + 1,
                    .MaximumEnergy = Function(l) l + 8,
                    .NameTable = Names.Fish,
                    .IsEnemy = True,
                    .AttackDice = "1d3/3+1d3/3",
                    .DefendDice = "1d6/6+1d6/6+1d6/6",
                    .FightEnergyCost = 1,
                    .CombatRestRoll = "1d2/2+1d2/2+1d2/2",
                    .SpawnCount = Function(difficulty)
                                      Select Case difficulty
                                          Case Difficulty.Yermom
                                              Return Function(x) x * 1 \ 1
                                          Case Difficulty.Easy
                                              Return Function(x) x * 1 \ 1
                                          Case Difficulty.Normal
                                              Return Function(x) x * 1 \ 1
                                          Case Difficulty.Difficult
                                              Return Function(x) x * 1 \ 1
                                          Case Difficulty.Too
                                              Return Function(x) x * 1 \ 1
                                          Case Else
                                              Throw New NotImplementedException
                                      End Select
                                  End Function,
                    .LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.FishScale, "2d2"}
                        },
                    .ExperiencePointValue = 1,
                    .ExperiencePointGoal = Function(x) 10 * (x + 1),
                    .ValidBribes = New HashSet(Of ItemType) From {ItemType.Food, ItemType.Potion, ItemType.Jools}
                }
            },
            {
                CharacterType.BossFish,
                New CharacterTypeDescriptor With
                {
                    .Name = "boss fish",
                    .MaximumHealth = Function(l) l + 5,
                    .MaximumEnergy = Function(l) l + 4,
                    .NameTable = Names.Fish,
                    .IsEnemy = True,
                    .AttackDice = "1d3/3+1d3/3+1d3/3+1d3/3+1d3/3",
                    .DefendDice = "1d6/6+1d6/6+1d6/6+1d6/6+1d6/6",
                    .FightEnergyCost = 1,
                    .CombatRestRoll = "1d2/2+1d2/2+1d2/2+1d2/2",
                    .SpawnCount = Function(difficulty)
                                      Select Case difficulty
                                          Case Difficulty.Yermom
                                              Return Function(x) 0
                                          Case Difficulty.Easy
                                              Return Function(x) 0
                                          Case Difficulty.Normal
                                              Return Function(x) 1
                                          Case Difficulty.Difficult
                                              Return Function(x) 2
                                          Case Difficulty.Too
                                              Return Function(x) 4
                                          Case Else
                                              Throw New NotImplementedException
                                      End Select
                                  End Function,
                    .LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.FishScale, "3d3"},
                            {ItemType.FishFin, "1d2/2"},
                            {ItemType.Jools, "1d1"}
                        },
                    .ExperiencePointValue = 5,
                    .ExperiencePointGoal = Function(x) 10 * (x + 1),
                    .ValidBribes = New HashSet(Of ItemType)
                }
            }
        }

    <Extension>
    Function Name(characterType As CharacterType) As String
        Return Descriptors(characterType).Name
    End Function

    <Extension>
    Function MaximumHealth(characterType As CharacterType, level As Long) As Long
        Return Descriptors(characterType).MaximumHealth(level)
    End Function

    <Extension>
    Function MaximumEnergy(characterType As CharacterType, level As Long) As Long
        Return Descriptors(characterType).MaximumEnergy(level)
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
    <Extension>
    Public Function SpawnCount(characterType As CharacterType, locationCount As Long, difficulty As Difficulty) As Long
        Return Descriptors(characterType).SpawnCount(difficulty)(locationCount)
    End Function

    <Extension>
    Public Function RandomName(characterType As CharacterType) As String
        Return RNG.FromList(Descriptors(characterType).NameTable)
    End Function

    <Extension>
    Public Function IsEnemy(characterType As CharacterType) As Boolean
        Return Descriptors(characterType).IsEnemy
    End Function

    <Extension>
    Public Function AttackDice(characterType As CharacterType) As String
        Return Descriptors(characterType).AttackDice
    End Function

    <Extension>
    Public Function DefendDice(characterType As CharacterType) As String
        Return Descriptors(characterType).DefendDice
    End Function

    <Extension>
    Public Function FightEnergyCost(characterType As CharacterType) As Long
        Return Descriptors(characterType).FightEnergyCost
    End Function

    <Extension>
    Public Function CombatRestRoll(characterType As CharacterType) As String
        Return Descriptors(characterType).CombatRestRoll
    End Function

    <Extension>
    Function LootDrops(characterType As CharacterType) As Dictionary(Of ItemType, String)
        Return Descriptors(characterType).LootDrops
    End Function

    <Extension>
    Function ExperiencePointValue(characterType As CharacterType, level As Long) As Long
        Return Descriptors(characterType).ExperiencePointValue
    End Function

    <Extension>
    Function ExperienceGoal(characterType As CharacterType, level As Long) As Long
        Return Descriptors(characterType).ExperiencePointGoal(level)
    End Function

    <Extension>
    Function TakesBribe(characterType As CharacterType, itemType As ItemType) As Boolean
        Return Descriptors(characterType).ValidBribes.Contains(itemType)
    End Function
End Module