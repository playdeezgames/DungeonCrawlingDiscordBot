Friend Class SnakeDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New(Faction.Vermin, "1d3/3")
        Name = "giant snake"
        Maximum = Function(s, c)
                      Select Case s
                          Case StatisticType.Energy
                              Return c.Level + 5
                          Case StatisticType.Health
                              Return c.Level + 1
                          Case Else
                              Return 0
                      End Select
                  End Function
        NameTable = Names.Snake
        AttackDice = "1d3/3"
        DefendDice = "1d6/6"
        FightEnergyCost = 1
        CombatRestRoll = "1d2/2+1d2/2"
        LootDrops = New Dictionary(Of ItemType, String) From
            {
                {ItemType.SnakeFang, "1d3/2"}
            }
        ExperiencePointValue = 0
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        SortOrder = 200
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) x * 3 \ 4},
            {Difficulty.Easy, Function(x) x * 2 \ 3},
            {Difficulty.Normal, Function(x) x * 1 \ 2},
            {Difficulty.Difficult, Function(x) x * 1 \ 2},
            {Difficulty.Too, Function(x) x * 1 \ 3}
        }
    Public Overrides Function SpawnLocations(difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Dim result As New List(Of Location)
        Dim candidates = locations
        Dim count = Faction.AdjustSpawnCountForTheme(theme, SpawnCountTable(difficulty)(candidates.LongCount))
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(candidates))
        End While
        Return result
    End Function
    Private Shared combatActionTable As New Dictionary(Of CombatActionType, Integer) From
        {
            {CombatActionType.Attack, 4},
            {CombatActionType.Poison, 1}
        }
    Public Overrides Function GenerateCombatAction(character As Character) As CombatActionType
        Return RNG.FromGenerator(combatActionTable)
    End Function
End Class
