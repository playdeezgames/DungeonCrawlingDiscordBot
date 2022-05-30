Imports System.Runtime.CompilerServices

Public Enum TerrainType
    None
    Forest
    Plains
    Hills
    Mountains
    Desert
    Swamp
End Enum
Public Module TerrainTypeExtensions
    Friend ReadOnly Property TerrainTypeGenerator As Dictionary(Of TerrainType, Integer)
        Get
            Return TerrainTypeDescriptors.ToDictionary(Function(x) x.Key, Function(x) x.Value.GeneratorWeight)
        End Get
    End Property
    <Extension>
    Friend Sub GenerateWanderingMonster(terrainType As TerrainType, location As Location)
        TerrainTypeDescriptors(terrainType).GenerateWanderingMonster(location)
    End Sub
    <Extension>
    Public Function Description(terrainType As TerrainType, character As Character) As String
        Return $"{character.FullName} is in {TerrainTypeDescriptors(terrainType).Description}."
    End Function
    <Extension>
    Public Function GeneratePeril(terrainType As TerrainType) As Long
        Return TerrainTypeDescriptors(terrainType).GeneratePeril
    End Function
    <Extension>
    Public Function GeneratePerilThreshold(terrainType As TerrainType) As Long
        Return TerrainTypeDescriptors(terrainType).GeneratePerilThreshold
    End Function
End Module
