Imports System.Runtime.CompilerServices

Public Enum IncentiveType
    None
    StartingFood
End Enum
Public Module IncentiveTypeExtensions
    ReadOnly Property AllIncentives As IEnumerable(Of IncentiveType)
        Get
            Return IncentiveTypeDescriptors.Keys
        End Get
    End Property
    <Extension>
    Function Name(incentiveType As IncentiveType) As String
        Return IncentiveTypeDescriptors(incentiveType).Name
    End Function
    <Extension>
    Function IncentivePrice(incentiveType As IncentiveType, level As Long) As Long
        Return IncentiveTypeDescriptors(incentiveType).IncentivePrice(level)
    End Function
End Module