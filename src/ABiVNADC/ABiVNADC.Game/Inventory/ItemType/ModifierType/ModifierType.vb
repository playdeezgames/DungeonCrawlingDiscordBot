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
    <Extension>
    Function Name(modifierType As ModifierType) As String
        Return ModifierTypeDescriptors(modifierType).Name
    End Function
End Module
