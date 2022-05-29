Public MustInherit Class TerrainTypeDescriptor
    ReadOnly Property GeneratorWeight As Integer
    ReadOnly Property Description As String
    Sub New(generatorWeight As Integer, description As String)
        Me.GeneratorWeight = generatorWeight
        Me.Description = description
    End Sub
    MustOverride Function GeneratePerilThreshold() As Long
    MustOverride Function GeneratePeril() As Long
End Class
Module TerrainTypeDescriptorUtility
    Friend ReadOnly TerrainTypeDescriptors As New Dictionary(Of TerrainType, TerrainTypeDescriptor) From
        {
            {
                TerrainType.Desert,
                New DesertDescriptor
            },
            {
                TerrainType.Forest,
                New ForestDescriptor
            },
            {
                TerrainType.Hills,
                New HillsDescriptor
            },
            {
                TerrainType.Mountains,
                New MountainsDescriptor
            },
            {
                TerrainType.Plains,
                New PlainsDescriptor
            },
            {
                TerrainType.Swamp,
                New SwampDescriptor
            }
        }
End Module