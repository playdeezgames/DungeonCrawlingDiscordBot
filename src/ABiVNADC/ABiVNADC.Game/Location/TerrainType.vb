Imports System.Runtime.CompilerServices

Public Enum TerrainType
    None
    Forest
    Plains
    Hills
    Mountains
    Desert
End Enum
Public Module TerrainTypeExtensions
    Friend ReadOnly TerrainTypeGenerator As New Dictionary(Of TerrainType, Integer) From
        {
            {TerrainType.Plains, 6},
            {TerrainType.Forest, 6},
            {TerrainType.Hills, 3},
            {TerrainType.Mountains, 2},
            {TerrainType.Desert, 1}
        }
    <Extension>
    Public Function Description(terrainType As TerrainType, character As Character) As String
        Select Case terrainType
            Case TerrainType.Desert
                Return $"{character.FullName} is in a sandy desert."
            Case TerrainType.Forest
                Return $"{character.FullName} is in a forest."
            Case TerrainType.Hills
                Return $"{character.FullName} is in a hilly area."
            Case TerrainType.Mountains
                Return $"{character.FullName} is in a mountainous area."
            Case TerrainType.Plains
                Return $"{character.FullName} is in a flat plains."
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
