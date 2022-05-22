Friend Class CrabDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New
        Name = "crab"
        Maximum = Function(s, c)
                      Select Case s
                          Case StatisticType.Energy
                              Return 1
                          Case StatisticType.Health
                              Return c.Level + 9
                          Case Else
                              Return 0
                      End Select
                  End Function
        NameTable = Names.Crab
        IsEnemy = True
        AttackDice = "1d3/3+1d3/3+1d3/3+1d3/3"
        DefendDice = "1d6/6+1d6/6+1d6/6+1d6/6+1d6/6"
        FightEnergyCost = 0
        CombatRestRoll = "0d1"
        SpawnCount = Function(difficulty)
                         Select Case difficulty
                             Case Difficulty.Yermom
                                 Return Function(x) 0
                             Case Difficulty.Easy
                                 Return Function(x) 0
                             Case Difficulty.Normal
                                 Return Function(x) 0
                             Case Difficulty.Difficult
                                 Return Function(x) 0
                             Case Difficulty.Too
                                 Return Function(x) 1
                             Case Else
                                 Throw New NotImplementedException
                         End Select
                     End Function
        LootDrops = New Dictionary(Of ItemType, String)
        ExperiencePointValue = 0
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        SortOrder = 1
    End Sub

End Class
