Imports System.Runtime.CompilerServices

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
    Rat
    Bat
    Snake
    Bug
End Enum
Public Module CharacterTypeExtensions
    <Extension>
    Function ModifyElementalDamage(characterType As CharacterType, elementalDamageType As ElementalDamageType, damage As Long) As Long
        Return CharacterTypeDescriptors(characterType).ModifyElementalDamage(elementalDamageType, damage)
    End Function
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
    Public Function SpawnLocations(characterType As CharacterType, difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Return CharacterTypeDescriptors(characterType).SpawnLocations(difficulty, theme, locations)
    End Function

    <Extension>
    Public Function RandomName(characterType As CharacterType) As String
        Return RNG.FromList(CharacterTypeDescriptors(characterType).NameTable)
    End Function

    <Extension>
    Public Function IsEnemy(characterType As CharacterType, candidate As Character) As Boolean
        Return CharacterTypeDescriptors(characterType).IsEnemy(candidate)
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
    Function SortOrder(characterType As CharacterType) As Long
        Return CharacterTypeDescriptors(characterType).SortOrder
    End Function

    <Extension>
    Function Faction(characterType As CharacterType) As Faction
        Return CharacterTypeDescriptors(characterType).Faction
    End Function

    <Extension>
    Function GenerateCombatAction(characterType As CharacterType, character As Character) As CombatActionType
        Return CharacterTypeDescriptors(characterType).GenerateCombatAction(character)
    End Function

    <Extension>
    Function PoisonDice(characterType As CharacterType) As String
        Return CharacterTypeDescriptors(characterType).PoisonDice
    End Function
End Module