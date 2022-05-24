Friend Class BossFishDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New(Faction.Fish)
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
        AttackDice = "1d3/3+1d3/3+1d3/3+1d3/3+1d3/3"
        DefendDice = "1d6/6+1d6/6+1d6/6+1d6/6+1d6/6"
        FightEnergyCost = 1
        CombatRestRoll = "1d2/2+1d2/2+1d2/2+1d2/2"
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
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Long) From
        {
            {Difficulty.Yermom, 0},
            {Difficulty.Easy, 0},
            {Difficulty.Normal, 1},
            {Difficulty.Difficult, 2},
            {Difficulty.Too, 4}
        }
    Public Overrides Function SpawnLocations(difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Dim result As New List(Of Location)
        Dim count = SpawnCountTable(difficulty)
        Dim deadEnds = locations.Where(Function(x) x.RouteCount = 1)
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(deadEnds))
        End While
        Return result
    End Function

End Class
