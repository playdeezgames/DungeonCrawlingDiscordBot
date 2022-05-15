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
    Friend ReadOnly Property TerrainTypeGenerator As Dictionary(Of TerrainType, Integer)
        Get
            Return TerrainTypeDescriptors.ToDictionary(Function(x) x.Key, Function(x) x.Value.GeneratorWeight)
        End Get
    End Property
    <Extension>
    Public Function Description(terrainType As TerrainType, character As Character) As String
        Return $"{character.FullName} is in {TerrainTypeDescriptors(terrainType).Description}"
    End Function
End Module
