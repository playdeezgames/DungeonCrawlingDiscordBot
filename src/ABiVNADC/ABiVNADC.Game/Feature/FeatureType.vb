Imports System.Runtime.CompilerServices

Public Enum FeatureType
    None
    DungeonExit
    DungeonEntrance
    Crossroads
    NorthSouthRoad
    EastWestRoad
    ForSaleSign
    Corpse
    ShoppeEntrance
    ShoppeExit
End Enum
Public Module FeatureTypeExtensions
    Friend ReadOnly OverworldFeatureGenerator As Dictionary(Of FeatureType, Integer) =
        FeatureTypeDescriptors.Where(
            Function(entry) entry.Value.OverworldGenerationWeight > 0).
            ToDictionary(
                Function(x) x.Key,
                Function(x) x.Value.OverworldGenerationWeight)
    <Extension>
    Function DungeonSpawnCount(featureType As FeatureType, locationCount As Long, difficulty As Difficulty) As String
        Return FeatureTypeDescriptors(featureType).DungeonSpawnDice(difficulty, locationCount)
    End Function
    <Extension>
    Function Name(featureType As FeatureType, feature As Feature) As String
        Return FeatureTypeDescriptors(featureType).FullName(feature)
    End Function

    Friend Sub GenerateForSaleSign(location As Location)
        FeatureData.Create(location.Id, FeatureType.ForSaleSign)
    End Sub

    Friend Sub GenerateShoppeEntrance(location As Location)
        Dim feature = New Feature(FeatureData.Create(location.Id, FeatureType.ShoppeEntrance))
        Dim insideLocation = New Location(LocationData.Create(LocationType.Shoppe))
        Dim shoppe = New Shoppe(ShoppeData.Create(GenerateShoppeName, location.Id, insideLocation.Id))
        ShoppeLocationData.Write(location.Id, shoppe.Id)
        ShoppeLocationData.Write(insideLocation.Id, shoppe.Id)
        FeatureData.Create(insideLocation.Id, FeatureType.ShoppeExit)
        For Each itemType In AllItemTypes
            Dim sellPrice As Long = If(RNG.FromGenerator(itemType.CanSellGenerator), RNG.RollDice(itemType.SellPriceDice), 0)
            Dim buyPrice As Long = If(RNG.FromGenerator(itemType.CanBuyGenerator), RNG.RollDice(itemType.BuyPriceDice), 0)
            ShoppePriceData.Write(shoppe.Id, itemType, buyPrice, sellPrice)
        Next
    End Sub

    Friend Sub GenerateEastWestRoad(location As Location)
        FeatureData.Create(location.Id, FeatureType.EastWestRoad)
    End Sub

    Friend Sub GenerateNorthSouthRoad(location As Location)
        FeatureData.Create(location.Id, FeatureType.NorthSouthRoad)
    End Sub

    Friend Sub GenerateCrossRoads(location As Location)
        FeatureData.Create(location.Id, FeatureType.Crossroads)
    End Sub

    Private ReadOnly dungeonDifficultyGenerator As New Dictionary(Of Difficulty, Integer) From
        {
            {Difficulty.Yermom, 16},
            {Difficulty.Easy, 8},
            {Difficulty.Normal, 4},
            {Difficulty.Difficult, 2},
            {Difficulty.Too, 1}
        }

    Private ReadOnly dungeonSizeGenerator As New Dictionary(Of Long, Integer) From
        {
            {4, 81},
            {6, 27},
            {8, 9},
            {12, 3},
            {16, 1}
        }

    Friend Sub GenerateDungeonEntrance(location As Location)
        FeatureData.Create(location.Id, FeatureType.DungeonEntrance)
        Dim dungeonSize = RNG.FromGenerator(dungeonSizeGenerator)
        Dim dungeon = Game.Dungeon.Create(Nothing, GenerateDungeonName, New Location(location.Id), dungeonSize, dungeonSize, RNG.FromGenerator(dungeonDifficultyGenerator))
        DungeonLocationData.Write(dungeon.Id, location.Id)
    End Sub
End Module