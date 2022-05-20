Imports System.Runtime.CompilerServices

Public Enum StatisticType
    None
    Health
    Energy
    Mana
    Guile
    Might
    Spirit
End Enum
Public Module StatisticTypeExtensions
    <Extension>
    Function Name(statisticType As StatisticType) As String
        Return StatisticTypeDescriptors(statisticType).Name
    End Function
End Module
