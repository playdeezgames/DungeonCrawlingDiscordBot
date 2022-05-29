Public Class StatisticTypeDescriptor
    Property Name As String
End Class
Friend Module StatisticTypeDescriptorUtility
    Friend ReadOnly StatisticTypeDescriptors As New Dictionary(Of StatisticType, StatisticTypeDescriptor) From
        {
            {
                StatisticType.Energy,
                New Game.StatisticTypeDescriptor With
                {
                    .Name = "Energy"
                }
            },
            {
                StatisticType.Guile,
                New Game.StatisticTypeDescriptor With
                {
                    .Name = "Guile"
                }
            },
            {
                StatisticType.Health,
                New Game.StatisticTypeDescriptor With
                {
                    .Name = "Health"
                }
            },
            {
                StatisticType.Mana,
                New Game.StatisticTypeDescriptor With
                {
                    .Name = "Mana"
                }
            },
            {
                StatisticType.Might,
                New Game.StatisticTypeDescriptor With
                {
                    .Name = "Might"
                }
            },
            {
                StatisticType.Spirit,
                New Game.StatisticTypeDescriptor With
                {
                    .Name = "Spirit"
                }
            }
        }
End Module