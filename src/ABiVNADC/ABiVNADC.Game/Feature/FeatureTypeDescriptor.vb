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
                    .FullName = Function(feature) "corpse"
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
                    .FullName = Function(feature) "dungeon entrance",
                    .OverworldGenerationWeight = 1
                }
            },
            {
                FeatureType.DungeonExit,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "dungeon exit",
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
                FeatureType.NorthSouthRoad,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "north-south road"
                }
            },
            {
                FeatureType.ShoppeEntrance,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "shoppe entrance",
                    .OverworldGenerationWeight = 1
                }
            },
            {
                FeatureType.ShoppeExit,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "shoppe exit"
                }
            },
            {
                FeatureType.ForSaleSign,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "for sale sign",
                    .OverworldGenerationWeight = 1
                }
            }
        }
End Module
