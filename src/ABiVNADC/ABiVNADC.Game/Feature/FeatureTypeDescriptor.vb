Public Class FeatureTypeDescriptor
    Property DungeonSpawnDice As Func(Of Difficulty, Long, String)
    Property OverworldGenerationWeight As Integer
    Property FullName As Func(Of Feature, String)
    Sub New()
        DungeonSpawnDice = Function(difficulty, locationCount) "0d1"
        OverworldGenerationWeight = 0
    End Sub
End Class
Module FeatureTypeDescriptorExtensions
    Friend ReadOnly FeatureTypeDescriptors As New Dictionary(Of FeatureType, FeatureTypeDescriptor) From
        {
            {
                FeatureType.Corpse,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature)
                                    Return $"the corpse(s) of {String.Join(", ", feature.Corpses.Select(Function(x) x.CorpseName))}"
                                End Function
                }
            },
            {
                FeatureType.Crossroads,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "crossroads"
                }
            },
            {
                FeatureType.DungeonEntrance,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"the entrance to {feature.Location.Dungeon.Name}",
                    .OverworldGenerationWeight = 1
                }
            },
            {
                FeatureType.DungeonExit,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"the exit from {feature.Location.Dungeon.Name}",
                    .DungeonSpawnDice = Function(difficulty, locationCount) "1d1"
                }
            },
            {
                FeatureType.EastWestRoad,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "east-west road"
                }
            },
            {
                FeatureType.Egress,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"exit from {feature.Egress.Name}"
                }
            },
            {
                FeatureType.Entrance,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"entrance to {feature.Entrance.Name}"
                }
            },
            {
                FeatureType.ForSaleSign,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature)
                                    Dim x = feature.Location.OverworldX.Value
                                    Dim y = feature.Location.OverworldY.Value
                                    Return $"a sign that reads `For Sale {If(y < 0, $"[N]{-y}", $"[S]{y}")}{If(x < 0, $"[W]{-x}", $"[E]{x}")}`"
                                End Function,
                    .OverworldGenerationWeight = 1
                }
            },
            {
                FeatureType.NorthSouthRoad,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "north-south road"
                }
            },
            {
                FeatureType.QuestGiver,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"quest giver named {feature.QuestGiver.Name}",
                    .OverworldGenerationWeight = 1
                }
            },
            {
                FeatureType.ShoppeEntrance,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"the entrance to {feature.Location.Shoppe.Name}",
                    .OverworldGenerationWeight = 1
                }
            },
            {
                FeatureType.ShoppeExit,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"the exit from {feature.Location.Shoppe.Name}"
                }
            },
            {
                FeatureType.VomitPuddle,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"a puddle of vomit"
                }
            }
        }
End Module
