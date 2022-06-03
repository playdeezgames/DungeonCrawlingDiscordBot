Imports System.Runtime.CompilerServices

Public Enum StatisticType
    None
    Health
    Energy
    Mana
    Guile
    Might
    Spirit
    Arseholes
End Enum
Public Module StatisticTypeExtensions
    <Extension>
    Function Name(statisticType As StatisticType) As String
        Return StatisticTypeDescriptors(statisticType).Name
    End Function
    <Extension>
    Function Format(statisticType As StatisticType, current As Long, maximum As Long) As String
        Return StatisticTypeDescriptors(statisticType).Format(current, maximum)
    End Function
End Module
