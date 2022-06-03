Friend Class GooseDescriptor
    Inherits CharacterTypeDescriptor

    Sub New()
        MyBase.New(Faction.WaterFowl, "0d1")
        Name = "goose"
        NameTable = Names.Geese
        AttackDice = "1d3/3+1d3/3+1d3/3"
        DefendDice = "1d6/6+1d6/6+1d6/6+1d6/6"
        FightEnergyCost = 1
        CombatRestRoll = "1d2/2+1d2/2+1d2/2+1d2/2"
        LootDrops = New Dictionary(Of ItemType, String) From
            {
                {ItemType.GooseFeather, "1d2"},
                {ItemType.GoosePoop, "2d3"},
                {ItemType.GooseEgg, "1d10/10"},
                {ItemType.GoldenEgg, "1d100/100"}
            }
        ExperiencePointValue = 1
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType) From {ItemType.Food, ItemType.Potion, ItemType.Jools}
        SortOrder = 100
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) 1},
            {Difficulty.Easy, Function(x) 2},
            {Difficulty.Normal, Function(x) 3},
            {Difficulty.Difficult, Function(x) 6},
            {Difficulty.Too, Function(x) 12}
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

    Public Overrides Function Maximum(statisticType As StatisticType, character As Character) As Long
        Select Case statisticType
            Case StatisticType.Energy
                Return character.Level + 10
            Case StatisticType.Health
                Return character.Level + 2
            Case StatisticType.Arseholes
                Return Long.MaxValue
            Case Else
                Return 0
        End Select
    End Function
End Class
