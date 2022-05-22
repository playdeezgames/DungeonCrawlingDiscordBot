Friend Class BossFishDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New
        Name = "boss fish"
        Maximum = Function(s, c)
                      Select Case s
                          Case StatisticType.Energy
                              Return c.Level + 4
                          Case StatisticType.Health
                              Return c.Level + 5
                          Case Else
                              Return 0
                      End Select
                  End Function
        NameTable = Names.Fish
        IsEnemy = True
        AttackDice = "1d3/3+1d3/3+1d3/3+1d3/3+1d3/3"
        DefendDice = "1d6/6+1d6/6+1d6/6+1d6/6+1d6/6"
        FightEnergyCost = 1
        CombatRestRoll = "1d2/2+1d2/2+1d2/2+1d2/2"
        SpawnCount = Function(difficulty)
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
                     End Function
        LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.FishScale, "3d3"},
                            {ItemType.FishFin, "1d2/2"},
                            {ItemType.Jools, "1d1"}
                        }
        ExperiencePointValue = 5
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        SortOrder = 5
    End Sub
End Class
