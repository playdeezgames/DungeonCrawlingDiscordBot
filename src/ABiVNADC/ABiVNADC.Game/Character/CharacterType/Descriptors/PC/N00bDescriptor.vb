Friend Class N00bDescriptor
    Inherits CharacterTypeDescriptor

    Sub New()
        MyBase.New(Faction.Player, "0d1")
        Name = "n00b"
        NameTable = Names.Human
        AttackDice = "1d6/6"
        DefendDice = "1d3/3+1d3/3"
        FightEnergyCost = 1
        CombatRestRoll = "1d1+1d2"
        LootDrops = New Dictionary(Of ItemType, String)
        ExperiencePointValue = 5
        ExperiencePointGoal = Function(x) 10 * (x + 1)
        ValidBribes = New HashSet(Of ItemType)
        MaximumEncumbrance = 50
    End Sub
    Public Overrides Function IncentiveValue(character As Character) As Long
        Dim level = character.Level
        Return (level + 1) * (level + 2) \ 2
    End Function

    Public Overrides Function Maximum(statisticType As StatisticType, character As Character) As Long
        Select Case statisticType
            Case StatisticType.Energy
                Return character.Level \ 2 + 7
            Case StatisticType.Health
                Return (character.Level + 1) \ 2 + 3
            Case StatisticType.Arseholes
                Return Long.MaxValue
            Case Else
                Return 0
        End Select
    End Function
End Class
