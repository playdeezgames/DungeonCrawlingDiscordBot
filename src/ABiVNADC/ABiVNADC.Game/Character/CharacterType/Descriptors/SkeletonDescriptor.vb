Friend Class SkeletonDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New
        Name = "skeleton"
        Maximum = Function(s, c)
                      Select Case s
                          Case StatisticType.Energy
                              Return 1
                          Case StatisticType.Health
                              Return c.Level + 1
                          Case Else
                              Return 0
                      End Select
                  End Function
        NameTable = Names.Human
        IsEnemy = True
        AttackDice = "1d3/3+1d3/3"
        DefendDice = "1d6/6+1d6/6"
        FightEnergyCost = 0
        CombatRestRoll = "0d1"
        SpawnCount = Function(difficulty)
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
                     End Function
        LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.SkullFragment, "1d4/4"}
                        }
        ExperiencePointValue = 1
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        SortOrder = 100
    End Sub
End Class
