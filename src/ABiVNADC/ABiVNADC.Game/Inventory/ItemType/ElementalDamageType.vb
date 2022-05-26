Imports System.Runtime.CompilerServices

Public Enum ElementalDamageType
    None
    Fire
End Enum
Public Module ElementalDamageTypeExtensions
    <Extension>
    Function Name(elementalDamageType As ElementalDamageType) As String
        Return ElementalDamageTypeDescriptors(elementalDamageType).Name
    End Function
End Module
