Imports System.Runtime.CompilerServices

Public Enum IncentiveType
    None
    StartingFood
End Enum
Public Module IncentiveTypeExtensions
    <Extension>
    Function Name(incentiveType As IncentiveType) As String
        Return IncentiveTypeDescriptors(incentiveType).Name
    End Function
End Module