Imports System.Runtime.CompilerServices

Public Enum ModifierType
    None
    Health
    Attack
    Defend
    Energy
End Enum
Module ModifierTypeExtensions
    <Extension>
    Function DecorateName(modifierType As ModifierType, name As String) As String
        Return ModifierTypeDescriptors(modifierType).NameDecorator(name)
    End Function
End Module
