Imports System.Runtime.CompilerServices

Public Enum FeatureType
    None
    DungeonExit
    DungeonEntrance
End Enum
Public Module FeatureTypeExtensions
    Friend ReadOnly AllDungeonFeatureTypes As New List(Of FeatureType) From
        {
            FeatureType.DungeonExit
        }
    Friend ReadOnly OverworldFeatureGenerator As New Dictionary(Of FeatureType, Integer) From
        {
            {FeatureType.None, 1},
            {FeatureType.DungeonEntrance, 1}
        }
    <Extension>
    Function DungeonSpawnCount(featureType As FeatureType, locationCount As Long, difficulty As Difficulty) As String
        Select Case featureType
            Case FeatureType.DungeonExit
                Return "1d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function Name(featureType As FeatureType) As String
        Select Case featureType
            Case FeatureType.DungeonExit
                Return "dungeon exit"
            Case FeatureType.DungeonEntrance
                Return "dungeon entrance"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module