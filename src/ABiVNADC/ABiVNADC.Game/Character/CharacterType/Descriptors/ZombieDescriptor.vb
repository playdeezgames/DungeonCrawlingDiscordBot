Friend Class ZombieDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New(Faction.Undead, "0d1")
        Name = "zombie"
        Maximum = Function(s, c)
                      Select Case s
                          Case StatisticType.Health
                              Return c.Level + 1
                          Case Else
                              Return 0
                      End Select
                  End Function
        NameTable = Names.Human
        AttackDice = "1d3/3+1d3/3"
        DefendDice = "1d6/6+1d6/6+1d6/6"
        FightEnergyCost = 0
        CombatRestRoll = "0d1"
        LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.ZombieTaint, "1d6/6"}
                        }
        ExperiencePointValue = 1
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        SortOrder = 50
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) x * 1 \ 6},
            {Difficulty.Easy, Function(x) x * 1 \ 4},
            {Difficulty.Normal, Function(x) x * 1 \ 3},
            {Difficulty.Difficult, Function(x) x * 1 \ 2},
            {Difficulty.Too, Function(x) x * 2 \ 3}
        }
    Public Overrides Function SpawnLocations(difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Dim result As New List(Of Location)
        Dim candidates = locations.Where(Function(x) x.RouteCount > 1)
        Dim count = Faction.AdjustSpawnCountForTheme(theme, SpawnCountTable(difficulty)(candidates.LongCount))
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(candidates))
        End While
        Return result
    End Function
End Class
