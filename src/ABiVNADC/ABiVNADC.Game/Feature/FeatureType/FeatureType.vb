Imports System.Runtime.CompilerServices

Public Enum FeatureType
    None
    DungeonEntrance
    Crossroads
    NorthSouthRoad
    EastWestRoad
    ForSaleSign
    Corpse
    ShoppeEntrance
    VomitPuddle
    QuestGiver
    Entrance
    Egress
    LandClaimOffice
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
    Function FullName(featureType As FeatureType, feature As Feature) As String
        Return FeatureTypeDescriptors(featureType).FullName(feature)
    End Function

    Friend Sub GenerateEastWestRoad(location As Location)
        FeatureData.Create(location.Id, FeatureType.EastWestRoad)
    End Sub

    Friend Sub GenerateNorthSouthRoad(location As Location)
        FeatureData.Create(location.Id, FeatureType.NorthSouthRoad)
    End Sub

    Friend Sub GenerateCrossRoads(location As Location)
        FeatureData.Create(location.Id, FeatureType.Crossroads)
    End Sub

    <Extension>
    Function Generate(featureType As FeatureType, location As Location) As Feature
        Return FeatureTypeDescriptors(featureType).Generator(location)
    End Function
End Module