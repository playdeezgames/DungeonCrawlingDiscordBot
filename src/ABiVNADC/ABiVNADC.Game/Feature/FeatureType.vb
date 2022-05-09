Imports System.Runtime.CompilerServices

Public Enum FeatureType
    None
    DungeonExit
    DungeonEntrance
    Crossroads
    NorthSouthRoad
    EastWestRoad
    ForSaleSign
End Enum
Public Module FeatureTypeExtensions
    Friend ReadOnly AllDungeonFeatureTypes As New List(Of FeatureType) From
        {
            FeatureType.DungeonExit
        }
    Friend ReadOnly OverworldFeatureGenerator As New Dictionary(Of FeatureType, Integer) From
        {
            {FeatureType.ForSaleSign, 1},
            {FeatureType.DungeonEntrance, 1}
        }
    <Extension>
    Function DungeonSpawnCount(featureType As FeatureType, locationCount As Long, difficulty As Difficulty) As String
        Select Case featureType
            Case FeatureType.DungeonExit
                Return "1d1"
            Case Else
                Return "0d1"
        End Select
    End Function
    <Extension>
    Function Name(featureType As FeatureType) As String
        Select Case featureType
            Case FeatureType.DungeonExit
                Return "dungeon exit"
            Case FeatureType.DungeonEntrance
                Return "dungeon entrance"
            Case FeatureType.Crossroads
                Return "crossroads"
            Case FeatureType.EastWestRoad
                Return "east-west road"
            Case FeatureType.NorthSouthRoad
                Return "north-south road"
            Case FeatureType.ForSaleSign
                Return "for sale sign"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Friend Sub GenerateForSaleSign(location As Location)
        FeatureData.Create(location.Id, FeatureType.ForSaleSign)
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