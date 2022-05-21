Friend Class CharacterTypeDescriptor
    Friend Name As String
    Friend Maximum As Func(Of StatisticType, Character, Long)
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
    Friend MaximumExperienceLevel As Long
    Friend MaximumEncumbrance As Long
    Friend CombatEndowmentRecoveryDice As String
    Friend CombatActionTable As Func(Of Character, Dictionary(Of CombatActionType, Integer))
    Sub New()
        MaximumExperienceLevel = Long.MaxValue
        MaximumEncumbrance = 0
        CombatEndowmentRecoveryDice = "0d1"
        Maximum = Function(s, c) 0
        CombatActionTable = Function(character)
                                If character.CanFight Then
                                    Return New Dictionary(Of CombatActionType, Integer) From
                                        {{CombatActionType.Attack, 1}}
                                Else
                                    Return New Dictionary(Of CombatActionType, Integer) From
                                        {{CombatActionType.Rest, 1}}
                                End If
                            End Function
    End Sub
End Class
Module CharacterTypeDescriptorExtensions
    Friend ReadOnly CharacterTypeDescriptors As New Dictionary(Of CharacterType, CharacterTypeDescriptor) From
        {
            {
                CharacterType.N00b,
                New CharacterTypeDescriptor With
                {
                    .Name = "n00b",
                    .Maximum = Function(s, c)
                                   Select Case s
                                       Case StatisticType.Energy
                                           Return c.Level + 7
                                       Case StatisticType.Health
                                           Return c.Level + 5
                                       Case Else
                                           Return 0
                                   End Select
                               End Function,
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
                    .ValidBribes = New HashSet(Of ItemType),
                    .MaximumEncumbrance = 50
                }
            },
            {
                CharacterType.Goblin,
                New CharacterTypeDescriptor With
                {
                    .Name = "goblin",
                    .Maximum = Function(s, c)
                                   Select Case s
                                       Case StatisticType.Energy
                                           Return c.Level + 10
                                       Case StatisticType.Health
                                           Return c.Level + 1
                                       Case Else
                                           Return 0
                                   End Select
                               End Function,
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
                CharacterType.Mummy,
                New CharacterTypeDescriptor With
                {
                    .Name = "mummy",
                    .Maximum = Function(s, c)
                                   Select Case s
                                       Case StatisticType.Energy
                                           Return 1
                                       Case StatisticType.Health
                                           Return c.Level + 2
                                       Case Else
                                           Return 0
                                   End Select
                               End Function,
                    .NameTable = Names.Human,
                    .IsEnemy = True,
                    .AttackDice = "1d3/3+1d3/3+1d3/3",
                    .DefendDice = "1d6/6+1d6/6+1d6/6+1d6/6",
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
                            {ItemType.Bandage, "2d2"},
                            {ItemType.Ankh, "1d20/20"}
                        },
                    .ExperiencePointValue = 1,
                    .ExperiencePointGoal = Function(x) 10 * (x + 1),
                    .ValidBribes = New HashSet(Of ItemType)
                }
            },
            {
                CharacterType.Orc,
                New CharacterTypeDescriptor With
                {
                    .Name = "orc",
                    .Maximum = Function(s, c)
                                   Select Case s
                                       Case StatisticType.Energy
                                           Return c.Level + 8
                                       Case StatisticType.Health
                                           Return c.Level + 1
                                       Case Else
                                           Return 0
                                   End Select
                               End Function,
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
                    .Maximum = Function(s, c)
                                   Select Case s
                                       Case StatisticType.Energy
                                           Return 1
                                       Case StatisticType.Health
                                           Return c.Level + 1
                                       Case Else
                                           Return 0
                                   End Select
                               End Function,
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
                    .Maximum = Function(s, c)
                                   Select Case s
                                       Case StatisticType.Energy
                                           Return 1
                                       Case StatisticType.Health
                                           Return c.Level + 1
                                       Case Else
                                           Return 0
                                   End Select
                               End Function,
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
                    .Maximum = Function(s, c)
                                   Select Case s
                                       Case StatisticType.Energy
                                           Return c.Level + 8
                                       Case StatisticType.Health
                                           Return c.Level + 1
                                       Case Else
                                           Return 0
                                   End Select
                               End Function,
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
                    .Maximum = Function(s, c)
                                   Select Case s
                                       Case StatisticType.Energy
                                           Return c.Level + 4
                                       Case StatisticType.Health
                                           Return c.Level + 5
                                       Case Else
                                           Return 0
                                   End Select
                               End Function,
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
End Module
