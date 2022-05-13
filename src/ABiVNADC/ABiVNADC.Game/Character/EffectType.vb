Imports System.Runtime.CompilerServices
Imports System.Text

Public Enum EffectType
    None
    Nausea
End Enum
Module EffectTypeExtensions
    <Extension>
    Sub ApplyOn(effectType As EffectType, character As Character, builder As StringBuilder)
        EffectTypeDescriptors(effectType).ApplyOn.Invoke(character, builder)
    End Sub
End Module