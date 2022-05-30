Public Class FeatureTypeDescriptor
    Property DungeonSpawnDice As Func(Of Difficulty, DungeonTheme, Long, String)
    Property OverworldGenerationWeight As Integer
    Property FullName As Func(Of Feature, String)
    Property Generator As Func(Of Location, Feature)
    Sub New()
        DungeonSpawnDice = Function(difficulty, theme, locationCount) "0d1"
        OverworldGenerationWeight = 0
    End Sub
End Class
Module FeatureTypeDescriptorUtility
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
                    .OverworldGenerationWeight = 8,
                    .Generator = Function(fromLocation)
                                     Dim dungeonDifficultyGenerator As New Dictionary(Of Difficulty, Integer) From
                                        {
                                            {Difficulty.Yermom, 16},
                                            {Difficulty.Easy, 8},
                                            {Difficulty.Normal, 4},
                                            {Difficulty.Difficult, 2},
                                            {Difficulty.Too, 1}
                                        }
                                     Dim dungeonSizeGenerator As New Dictionary(Of Long, Integer) From
                                        {
                                            {4, 81},
                                            {6, 27},
                                            {8, 9},
                                            {12, 3},
                                            {16, 1}
                                        }
                                     Dim dungeonSize = RNG.FromGenerator(dungeonSizeGenerator)
                                     Dim dungeon = Game.Dungeon.Create(GenerateDungeonName, New Location(fromLocation.Id), dungeonSize, dungeonSize, RNG.FromGenerator(dungeonDifficultyGenerator))

                                     Dim result = Entrance.Create(fromLocation, dungeon.Name)

                                     Egress.Create(dungeon.StartingLocation, dungeon.Name)

                                     Route.Create(fromLocation, Direction.Inward, dungeon.StartingLocation)
                                     Route.Create(dungeon.StartingLocation, Direction.Outward, fromLocation)

                                     Return New Feature(result.Id)
                                 End Function
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
                                    Dim x = feature.Location.Overworld.X
                                    Dim y = feature.Location.Overworld.Y
                                    Return $"a sign that reads `For Sale {If(y < 0, $"[N]{-y}", $"[S]{y}")}{If(x < 0, $"[W]{-x}", $"[E]{x}")}`"
                                End Function,
                    .OverworldGenerationWeight = 16,
                    .Generator = MakeGenerator(FeatureType.ForSaleSign)
                }
            },
            {
                FeatureType.HomeStone,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature)
                                    Dim owner = feature.Owner
                                    If owner Is Nothing Then
                                        Return $"an unowned home stone"
                                    End If
                                    Return $"a home stone owned by {owner.FullName}"
                                End Function,
                    .OverworldGenerationWeight = 0,
                    .Generator = MakeGenerator(FeatureType.HomeStone)
                }
            },
            {
                FeatureType.IncentivesOffice,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "incentives office",
                    .OverworldGenerationWeight = 1,
                    .Generator = Function(fromLocation)
                                     Dim toLocation = New Location(LocationData.Create(LocationType.IncentivesOffice))
                                     Dim entrance = Game.Entrance.Create(fromLocation, "Incentives Office")
                                     Egress.Create(toLocation, "Incentives Office")
                                     Route.Create(fromLocation, Direction.Inward, toLocation)
                                     Route.Create(toLocation, Direction.Outward, fromLocation)
                                     Return Feature.Create(toLocation, FeatureType.IncentivesOffice)
                                 End Function
                }
            },
            {
                FeatureType.LandClaimOffice,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) "land claim office",
                    .OverworldGenerationWeight = 1,
                    .Generator = Function(fromLocation)
                                     Dim toLocation = New Location(LocationData.Create(LocationType.LandClaimOffice))
                                     Dim entrance = Game.Entrance.Create(fromLocation, "Land Claim Office")
                                     Egress.Create(toLocation, "Land Claim Office")
                                     Dim office = LandClaimOffice.Create(toLocation)
                                     Route.Create(fromLocation, Direction.Inward, toLocation)
                                     Route.Create(toLocation, Direction.Outward, fromLocation)
                                     Return New Feature(office.Id)
                                 End Function
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
                    .OverworldGenerationWeight = 4,
                    .Generator = Function(location)
                                     Dim feature = Game.Feature.Create(location, FeatureType.QuestGiver)
                                     Dim targetItemType = RNG.FromGenerator(QuestTargetGenerator)
                                     Dim rewardItemType = RNG.FromGenerator(QuestRewardGenerator)
                                     Dim name = Names.GenerateQuestGiverName
                                     QuestGiver.Create(
                                        feature.Id,
                                        name,
                                        targetItemType,
                                        RNG.RollDice(targetItemType.QuestTargetQuantityDice),
                                        rewardItemType,
                                        RNG.RollDice(rewardItemType.QuestRewardQuantityDice))
                                     Return feature
                                 End Function
                }
            },
            {
                FeatureType.ShoppeEntrance,
                New FeatureTypeDescriptor With
                {
                    .FullName = Function(feature) $"the entrance to {feature.Location.Shoppe.Name}",
                    .OverworldGenerationWeight = 2,
                    .Generator = Function(fromLocation)
                                     Dim toLocation = New Location(LocationData.Create(LocationType.Shoppe))
                                     Dim shoppe = New Shoppe(ShoppeData.Create(GenerateShoppeName, fromLocation.Id, toLocation.Id))
                                     ShoppeLocationData.Write(toLocation.Id, shoppe.Id)

                                     Dim result = Entrance.Create(fromLocation, shoppe.Name)

                                     Egress.Create(toLocation, shoppe.Name)

                                     Route.Create(fromLocation, Direction.Inward, toLocation)
                                     Route.Create(toLocation, Direction.Outward, fromLocation)
                                     For Each itemType In AllItemTypes
                                         Dim sellPrice As Long = If(RNG.FromGenerator(itemType.CanSellGenerator), RNG.RollDice(itemType.SellPriceDice), 0)
                                         Dim buyPrice As Long = If(RNG.FromGenerator(itemType.CanBuyGenerator), RNG.RollDice(itemType.BuyPriceDice), 0)
                                         ShoppePriceData.Write(shoppe.Id, itemType, buyPrice, sellPrice)
                                     Next
                                     Return New Feature(result.Id)
                                 End Function
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
