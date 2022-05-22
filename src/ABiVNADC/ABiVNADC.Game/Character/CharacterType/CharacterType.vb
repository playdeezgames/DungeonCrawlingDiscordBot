﻿Imports System.Runtime.CompilerServices

Public Enum CharacterType
    None
    N00b
    Goblin
    Orc
    Skeleton
    Zombie
    MinionFish
    BossFish
    Mummy
    Crab
End Enum
Public Module CharacterTypeExtensions
    <Extension>
    Function InfectionDice(characterType As CharacterType) As String
        Return CharacterTypeDescriptors(characterType).InfectionDice
    End Function

    <Extension>
    Function Name(characterType As CharacterType) As String
        Return CharacterTypeDescriptors(characterType).Name
    End Function

    <Extension>
    Function Maximum(characterType As CharacterType, statisticType As StatisticType, character As Character) As Long
        Return CharacterTypeDescriptors(characterType).Maximum(statisticType, character)
    End Function

    Public Function AllCharacterTypes() As IEnumerable(Of CharacterType)
        Return CharacterTypeDescriptors.Keys
    End Function
    <Extension>
    Public Function SpawnCount(characterType As CharacterType, locationCount As Long, difficulty As Difficulty) As Long
        Return CharacterTypeDescriptors(characterType).SpawnCount(difficulty)(locationCount)
    End Function

    <Extension>
    Public Function RandomName(characterType As CharacterType) As String
        Return RNG.FromList(CharacterTypeDescriptors(characterType).NameTable)
    End Function

    <Extension>
    Public Function IsEnemy(characterType As CharacterType) As Boolean
        Return CharacterTypeDescriptors(characterType).IsEnemy
    End Function

    <Extension>
    Public Function AttackDice(characterType As CharacterType) As String
        Return CharacterTypeDescriptors(characterType).AttackDice
    End Function

    <Extension>
    Public Function DefendDice(characterType As CharacterType) As String
        Return CharacterTypeDescriptors(characterType).DefendDice
    End Function

    <Extension>
    Public Function FightEnergyCost(characterType As CharacterType) As Long
        Return CharacterTypeDescriptors(characterType).FightEnergyCost
    End Function

    <Extension>
    Public Function CombatRestRoll(characterType As CharacterType) As String
        Return CharacterTypeDescriptors(characterType).CombatRestRoll
    End Function

    <Extension>
    Function LootDrops(characterType As CharacterType) As Dictionary(Of ItemType, String)
        Return CharacterTypeDescriptors(characterType).LootDrops
    End Function

    <Extension>
    Function ExperiencePointValue(characterType As CharacterType, level As Long) As Long
        Return CharacterTypeDescriptors(characterType).ExperiencePointValue
    End Function

    <Extension>
    Function ExperienceGoal(characterType As CharacterType, level As Long) As Long
        Return CharacterTypeDescriptors(characterType).ExperiencePointGoal(level)
    End Function

    <Extension>
    Function MaximumLevel(characterType As CharacterType) As Long
        Return CharacterTypeDescriptors(characterType).MaximumExperienceLevel
    End Function

    <Extension>
    Function TakesBribe(characterType As CharacterType, itemType As ItemType) As Boolean
        Return CharacterTypeDescriptors(characterType).ValidBribes.Contains(itemType)
    End Function

    <Extension>
    Function MaximumEncumbrance(characterType As CharacterType) As Long
        Return CharacterTypeDescriptors(characterType).MaximumEncumbrance
    End Function

    <Extension>
    Function CombatActionTable(characterType As CharacterType, character As Character) As Dictionary(Of CombatActionType, Integer)
        Return CharacterTypeDescriptors(characterType).CombatActionTable(character)
    End Function

    <Extension>
    Function SortOrder(characterType As CharacterType) As Long
        Return CharacterTypeDescriptors(characterType).SortOrder
    End Function
End Module