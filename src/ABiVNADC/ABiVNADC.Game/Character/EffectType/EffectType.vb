Imports System.Runtime.CompilerServices
Imports System.Text

Public Enum EffectType
    None
    Nausea
    Infection
    Groggy
    Shrapnel
End Enum
Public Module EffectTypeExtensions
    <Extension>
    Function Name(effectType As EffectType) As String
        Return EffectTypeDescriptors(effectType).Name
    End Function
    <Extension>
    Sub ApplyOn(effectType As EffectType, character As Character, builder As StringBuilder)
        EffectTypeDescriptors(effectType).ApplyOn.Invoke(character, builder)
    End Sub
    <Extension>
    Function HasAttackDice(effectType As EffectType) As Boolean
        Return effectType.AttackDice <> "0d1"
    End Function
    <Extension>
    Function AttackDice(effectType As EffectType) As String
        Return EffectTypeDescriptors(effectType).AttackDice
    End Function
    <Extension>
    Function HasDefendDice(effectType As EffectType) As Boolean
        Return effectType.DefendDice <> "0d1"
    End Function
    <Extension>
    Function DefendDice(effectType As EffectType) As String
        Return EffectTypeDescriptors(effectType).DefendDice
    End Function
End Module