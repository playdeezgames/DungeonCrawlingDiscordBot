Public MustInherit Class StatisticTypeDescriptor
    Public MustOverride ReadOnly Property Name As String

    Overridable Function Format(current As Long, maximum As Long) As String
        Return $"{current}/{maximum}"
    End Function
End Class
Friend Module StatisticTypeDescriptorUtility
    Friend ReadOnly StatisticTypeDescriptors As New Dictionary(Of StatisticType, StatisticTypeDescriptor) From
        {
            {
                StatisticType.Arseholes,
                New ArseholesStatisticDescriptor
            },
            {
                StatisticType.Energy,
                New EnergyStatisticDescriptor
            },
            {
                StatisticType.Guile,
                New GuileStatisticDescriptor
            },
            {
                StatisticType.Health,
                New HealthStatisticDescriptor
            },
            {
                StatisticType.Mana,
                New ManaStatisticDescriptor
            },
            {
                StatisticType.Might,
                New MightStatisticDescriptor
            },
            {
                StatisticType.Spirit,
                New SpiritStatisticDescriptor
            }
        }
End Module