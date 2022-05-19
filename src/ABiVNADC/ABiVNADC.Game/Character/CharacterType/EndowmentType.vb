Imports System.Runtime.CompilerServices

Public Enum EndowmentType
    None
    Might
    Spirit
    Guile
    Mana
End Enum
Module EndowmentTypeExtensions
    <Extension>
    Function Name(endowmentType As EndowmentType) As String
        Return EndowmentTypeDescriptors(endowmentType).Name
    End Function
End Module
