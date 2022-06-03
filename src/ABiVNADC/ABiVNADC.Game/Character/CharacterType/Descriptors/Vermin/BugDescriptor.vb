Friend Class BugDescriptor
    Inherits CharacterTypeDescriptor

    Sub New()
        MyBase.New(Faction.Vermin, "0d1")
        Name = "giant bug"
        NameTable = Names.Bug
        AttackDice = "1d3/3"
        DefendDice = "1d6/6"
        FightEnergyCost = 1
        CombatRestRoll = "1d2/2+1d2/2"
        LootDrops = New Dictionary(Of ItemType, String) From
            {
                {ItemType.BugGuts, "1d2/2"}
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
    Public Overrides Function AdjustEffectDuration(character As Character, effectType As EffectType, duration As Long) As Long
        Select Case effectType
            Case EffectType.Nausea
                Return 0
            Case Else
                Return duration
        End Select
    End Function

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
                Return character.Level + 8
            Case StatisticType.Health
                Return character.Level + 1
            Case StatisticType.Arseholes
                Return Long.MaxValue
            Case Else
                Return 0
        End Select
    End Function
End Class
