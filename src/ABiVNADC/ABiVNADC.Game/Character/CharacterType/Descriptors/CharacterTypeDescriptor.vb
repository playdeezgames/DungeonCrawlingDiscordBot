Friend MustInherit Class CharacterTypeDescriptor
    MustOverride Function Maximum(statisticType As StatisticType, character As Character) As Long
    Overridable Function Initial(statisticType As StatisticType) As Long
        Select Case statisticType
            Case StatisticType.Arseholes
                Return Long.MaxValue - 1
            Case Else
                Return 0
        End Select
    End Function
    ReadOnly Property Faction As Faction
    ReadOnly Property PoisonDice As String
    Friend Name As String
    Friend NameTable As List(Of String)
    Friend AttackDice As String
    Friend DefendDice As String
    Friend FightEnergyCost As Long
    Friend CombatRestRoll As String
    Friend LootDrops As Dictionary(Of ItemType, String)
    Friend ExperiencePointValue As Long

    Friend Overridable Function ModifyElementalDamage(elementalDamageType As ElementalDamageType, damage As Long) As Long
        Return damage
    End Function

    Friend ExperiencePointGoal As Func(Of Long, Long)
    Friend ValidBribes As HashSet(Of ItemType)
    Friend MaximumExperienceLevel As Long
    Friend MaximumEncumbrance As Long
    Friend CombatEndowmentRecoveryDice As String
    Friend SortOrder As Long
    Friend InfectionDice As String
    Overridable Function GenerateCombatAction(character As Character) As CombatActionType
        If character.CanFight Then
            Return CombatActionType.Attack
        Else
            Return CombatActionType.Rest
        End If
    End Function
    Overridable Function SpawnLocations(difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Return New List(Of Location)
    End Function
    Sub New(faction As Faction, poisonDice As String)
        Me.Faction = faction
        Me.PoisonDice = poisonDice
        MaximumExperienceLevel = Long.MaxValue
        MaximumEncumbrance = 0
        CombatEndowmentRecoveryDice = "0d1"
        InfectionDice = "0d1"
    End Sub
    Function IsEnemy(candidate As Character) As Boolean
        Return candidate.Exists AndAlso Faction.IsEnemy(candidate.Faction)
    End Function

    Overridable Function IncentiveValue(character As Character) As Long
        Return 0
    End Function

    Overridable Function AdjustEffectDuration(character As Character, effectType As EffectType, duration As Long) As Long
        Return duration
    End Function
End Class
Module CharacterTypeDescriptorUtility
    Friend ReadOnly CharacterTypeDescriptors As New Dictionary(Of CharacterType, CharacterTypeDescriptor) From
        {
            {
                CharacterType.Bat,
                New BatDescriptor
            },
            {
                CharacterType.BossFish,
                New BossFishDescriptor
            },
            {
                CharacterType.Bug,
                New BugDescriptor
            },
            {
                CharacterType.Crab,
                New CrabDescriptor
            },
            {
                CharacterType.Goblin,
                New GoblinDescriptor
            },
            {
                CharacterType.Goose,
                New GooseDescriptor
            },
            {
                CharacterType.MinionFish,
                New MinionFishDescriptor
            },
            {
                CharacterType.Mummy,
                New MummyDescriptor
            },
            {
                CharacterType.N00b,
                New N00bDescriptor
            },
            {
                CharacterType.Orc,
                New OrcDescriptor
            },
            {
                CharacterType.Rat,
                New RatDescriptor
            },
            {
                CharacterType.Skeleton,
                New SkeletonDescriptor
            },
            {
                CharacterType.Snake,
                New SnakeDescriptor
            },
            {
                CharacterType.Zombie,
                New ZombieDescriptor
            }
        }
End Module
