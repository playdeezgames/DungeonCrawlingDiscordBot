Imports System.Runtime.CompilerServices

Public Enum FeatureType
    None
    DungeonExit
End Enum
Public Module FeatureTypeExtensions
    Public ReadOnly AllFeatureTypes As New List(Of FeatureType) From
        {
            FeatureType.DungeonExit
        }
    <Extension>
    Function SpawnCount(featureType As FeatureType, locationCount As Long, difficulty As Difficulty) As String
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
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module