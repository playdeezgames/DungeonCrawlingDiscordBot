Friend Class CrabDescriptor
    Inherits CharacterTypeDescriptor
    Sub New()
        MyBase.New(Faction.Monster, "0d1")
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
    Public Overrides Function SpawnLocations(difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Dim result As New List(Of Location)
        If difficulty = Difficulty.Too Then
            result.Add(RNG.FromEnumerable(locations.Where(Function(x) x.RouteCount = 1)))
        End If
        Return result
    End Function
    Public Overrides Function AdjustEffectDuration(character As Character, effectType As EffectType, duration As Long) As Long
        Return 0
    End Function
    Friend Overrides Function ModifyElementalDamage(elementalDamageType As ElementalDamageType, damage As Long) As Long
        Select Case elementalDamageType
            Case ElementalDamageType.Fire
                Return 0
            Case Else
                Return damage
        End Select
    End Function
End Class
