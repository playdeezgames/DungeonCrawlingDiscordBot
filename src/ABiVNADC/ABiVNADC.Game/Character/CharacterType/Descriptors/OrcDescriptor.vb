Friend Class OrcDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New
        Name = "orc"
        Maximum = Function(s, c)
                      Select Case s
                          Case StatisticType.Energy
                              Return c.Level + 8
                          Case StatisticType.Health
                              Return c.Level + 1
                          Case Else
                              Return 0
                      End Select
                  End Function
        NameTable = Names.Orc
        IsEnemy = True
        AttackDice = "1d3/3+1d3/3+1d3/3"
        DefendDice = "1d6/6+1d6/6"
        FightEnergyCost = 1
        CombatRestRoll = "1d2/2+1d2/2+1d2/2"
        SpawnCount = Function(difficulty)
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
                     End Function
        LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.Food, "1d2/2"},
                            {ItemType.OrcTooth, "1d3/3"}
                        }
        ExperiencePointValue = 1
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType) From {ItemType.Food, ItemType.Potion, ItemType.Jools}
        SortOrder = 50
    End Sub
End Class
