Friend Class N00bDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New
        Name = "n00b"
        Maximum = Function(s, c)
                      Select Case s
                          Case StatisticType.Energy
                              Return c.Level \ 2 + 7
                          Case StatisticType.Health
                              Return (c.Level + 1) \ 2 + 3
                          Case Else
                              Return 0
                      End Select
                  End Function
        NameTable = Names.Human
        IsEnemy = False
        AttackDice = "1d3/3"
        DefendDice = "1d3/3+1d3/3"
        FightEnergyCost = 1
        CombatRestRoll = "1d1+1d2/2"
        LootDrops = New Dictionary(Of ItemType, String)
        ExperiencePointValue = 5
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        MaximumEncumbrance = 50
    End Sub
End Class
