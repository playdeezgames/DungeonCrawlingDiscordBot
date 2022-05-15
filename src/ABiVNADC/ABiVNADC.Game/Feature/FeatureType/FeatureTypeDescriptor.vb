Public Class FeatureTypeDescriptor
    Property DungeonSpawnDice As Func(Of Difficulty, Long, String)
    Property OverworldGenerationWeight As Integer
    Property FullName As Func(Of Feature, String)
    Property Generator As Func(Of Location, Feature)
    Sub New()
        DungeonSpawnDice = Function(difficulty, locationCount) "0d1"
        OverworldGenerationWeight = 0
    End Sub
End Class
Module FeatureTypeDescriptorExtensions
    Private Function MakeGenerator(featureType As FeatureType) As Func(Of Location, Feature)
        Return Function(location)
                   Return Feature.Create(location, featureType)
               End Function
    End Function
    Friend ReadOnly FeatureTypeDescriptors As New Dictionary(Of FeatureType, FeatureTypeDescriptor) From
        {
            {
                FeatureType.Corpse,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature)
                                    Return $"the corpse(s) of {String.Join(", ", feature.Corpses.Select(Function(x) x.CorpseName))}"
                                End Function,
                    .Generator = MakeGenerator(FeatureType.Corpse)
                }
            },
            {
                FeatureType.Crossroads,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "crossroads",
                    .Generator = MakeGenerator(FeatureType.Crossroads)
                }
            },
            {
                FeatureType.DungeonEntrance,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"the entrance to {feature.Location.Dungeon.Name}",
                    .OverworldGenerationWeight = 1,
                    .Generator = MakeGenerator(FeatureType.DungeonEntrance)
                }
            },
            {
                FeatureType.EastWestRoad,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "east-west road",
                    .Generator = MakeGenerator(FeatureType.EastWestRoad)
                }
            },
            {
                FeatureType.Egress,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"exit from {feature.Egress.Name}",
                    .Generator = MakeGenerator(FeatureType.Egress)
                }
            },
            {
                FeatureType.Entrance,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"entrance to {feature.Entrance.Name}",
                    .Generator = MakeGenerator(FeatureType.Entrance)
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
                    .OverworldGenerationWeight = 1,
                    .Generator = MakeGenerator(FeatureType.ForSaleSign)
                }
            },
            {
                FeatureType.LandClaimOffice,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "land claim office",
                    .OverworldGenerationWeight = 1,
                    .Generator = MakeGenerator(FeatureType.LandClaimOffice)
                }
            },
            {
                FeatureType.NorthSouthRoad,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "north-south road",
                    .Generator = MakeGenerator(FeatureType.NorthSouthRoad)
                }
            },
            {
                FeatureType.QuestGiver,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"quest giver named {feature.QuestGiver.Name}",
                    .OverworldGenerationWeight = 1,
                    .Generator = MakeGenerator(FeatureType.QuestGiver)
                }
            },
            {
                FeatureType.ShoppeEntrance,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"the entrance to {feature.Location.Shoppe.Name}",
                    .OverworldGenerationWeight = 1,
                    .Generator = MakeGenerator(FeatureType.ShoppeEntrance)
                }
            },
            {
                FeatureType.VomitPuddle,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"a puddle of vomit",
                    .Generator = MakeGenerator(FeatureType.VomitPuddle)
                }
            }
        }
End Module
