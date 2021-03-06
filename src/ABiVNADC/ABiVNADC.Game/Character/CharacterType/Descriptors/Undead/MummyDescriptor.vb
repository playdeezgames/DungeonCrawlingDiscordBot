Friend Class MummyDescriptor
    Inherits CharacterTypeDescriptor

    Sub New()
        MyBase.New(Faction.Undead, "0d1")
        Name = "mummy"
        NameTable = Names.Human
        AttackDice = "1d3/3+1d3/3+1d3/3"
        DefendDice = "1d6/6+1d6/6+1d6/6+1d6/6"
        FightEnergyCost = 0
        CombatRestRoll = "0d1"
        LootDrops = New Dictionary(Of ItemType, String) From
                        {
                            {ItemType.Bandage, "2d2"},
                            {ItemType.Ankh, "1d20/20"}
                        }
        ExperiencePointValue = 1
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        SortOrder = 10
        InfectionDice = "1d6"
    End Sub
    Public Overrides Function AdjustEffectDuration(character As Character, effectType As EffectType, duration As Long) As Long
        Return If(effectType = EffectType.Groggy OrElse effectType = EffectType.Nausea, 0, duration)
    End Function
    Private Shared combatActionTable As New Dictionary(Of CombatActionType, Integer) From
                                                 {
                                                    {CombatActionType.Attack, 4},
                                                    {CombatActionType.Infect, 1}
                                                 }
    Public Overrides Function GenerateCombatAction(character As Character) As CombatActionType
        Return RNG.FromGenerator(combatActionTable)
    End Function
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
        Dim candidates = locations.Where(Function(x) x.RouteCount = 1)
        Dim count = Faction.AdjustSpawnCountForTheme(theme, SpawnCountTable(difficulty)(candidates.LongCount))
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(candidates))
        End While
        Return result
    End Function
    Friend Overrides Function ModifyElementalDamage(elementalDamageType As ElementalDamageType, damage As Long) As Long
        Select Case elementalDamageType
            Case ElementalDamageType.Fire
                Return damage * 2
            Case Else
                Return damage
        End Select
    End Function

    Public Overrides Function Maximum(statisticType As StatisticType, character As Character) As Long
        Select Case statisticType
            Case StatisticType.Health
                Return character.Level + 2
            Case StatisticType.Arseholes
                Return Long.MaxValue
            Case Else
                Return 0
        End Select
    End Function
End Class
