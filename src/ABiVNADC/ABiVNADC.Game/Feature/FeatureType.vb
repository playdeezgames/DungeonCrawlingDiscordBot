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

    Friend Sub GenerateDungeonEntrance(location As Location)
        FeatureData.Create(location.Id, FeatureType.DungeonEntrance)
        Dim dungeon = Game.Dungeon.Create(Nothing, GenerateDungeonName, New Location(location.Id), 4, 4, Difficulty.Yermom)
        DungeonLocationData.Write(dungeon.Id, location.Id)
    End Sub
End Module