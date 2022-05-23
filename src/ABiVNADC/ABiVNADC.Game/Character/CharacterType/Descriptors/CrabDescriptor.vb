Friend Class CrabDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New(Faction.Monster)
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
        LootDrops = New Dictionary(Of ItemType, String)
        ExperiencePointValue = 0
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        SortOrder = 1
    End Sub
    Public Overrides Function SpawnLocations(difficulty As Difficulty, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Dim result As New List(Of Location)
        If difficulty = Difficulty.Too Then
            result.Add(RNG.FromEnumerable(locations.Where(Function(x) x.RouteCount = 1)))
        End If
        Return result
    End Function
End Class
