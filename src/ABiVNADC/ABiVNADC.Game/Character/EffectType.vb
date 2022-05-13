Imports System.Runtime.CompilerServices
Imports System.Text

Public Enum EffectType
    None
    Nausea
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
End Module