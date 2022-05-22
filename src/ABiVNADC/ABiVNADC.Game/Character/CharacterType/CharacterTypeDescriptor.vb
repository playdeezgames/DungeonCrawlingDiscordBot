﻿Friend Class CharacterTypeDescriptor
    Friend Name As String
    Friend Maximum As Func(Of StatisticType, Character, Long)
    Friend NameTable As List(Of String)
    Friend IsEnemy As Boolean
    Friend AttackDice As String
    Friend DefendDice As String
    Friend FightEnergyCost As Long
    Friend CombatRestRoll As String
    Friend LootDrops As Dictionary(Of ItemType, String)
    Friend ExperiencePointValue As Long
    Friend ExperiencePointGoal As Func(Of Long, Long)
    Friend ValidBribes As HashSet(Of ItemType)
    Friend MaximumExperienceLevel As Long
    Friend MaximumEncumbrance As Long
    Friend CombatEndowmentRecoveryDice As String
    Friend CombatActionTable As Func(Of Character, Dictionary(Of CombatActionType, Integer))
    Friend SortOrder As Long
    Friend InfectionDice As String
    Overridable Function SpawnLocations(difficulty As Difficulty, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Return New List(Of Location)
    End Function
    Sub New()
        MaximumExperienceLevel = Long.MaxValue
        MaximumEncumbrance = 0
        CombatEndowmentRecoveryDice = "0d1"
        Maximum = Function(s, c) 0
        InfectionDice = "0d1"
        CombatActionTable = Function(character)
                                If character.CanFight Then
                                    Return New Dictionary(Of CombatActionType, Integer) From
                                        {{CombatActionType.Attack, 1}}
                                Else
                                    Return New Dictionary(Of CombatActionType, Integer) From
                                        {{CombatActionType.Rest, 1}}
                                End If
                            End Function
    End Sub
End Class
Module CharacterTypeDescriptorExtensions
    Friend ReadOnly CharacterTypeDescriptors As New Dictionary(Of CharacterType, CharacterTypeDescriptor) From
        {
            {
                CharacterType.BossFish,
                New BossFishDescriptor
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
                CharacterType.Skeleton,
                New SkeletonDescriptor
            },
            {
                CharacterType.Zombie,
                New ZombieDescriptor
            }
        }
End Module
