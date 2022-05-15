Public Class TerrainTypeDescriptor
    Property GeneratorWeight As Integer
    Property Description As String
End Class
Module TerrainTypeDescriptorExtensions
    Friend ReadOnly TerrainTypeDescriptors As New Dictionary(Of TerrainType, TerrainTypeDescriptor) From
        {
            {
                TerrainType.Desert,
                New TerrainTypeDescriptor With
                {
                    .GeneratorWeight = 1,
                    .Description = "a sandy desert"
                }
            },
            {
                TerrainType.Forest,
                New TerrainTypeDescriptor With
                {
                    .GeneratorWeight = 6,
                    .Description = "a forest"
                }
            },
            {
                TerrainType.Hills,
                New TerrainTypeDescriptor With
                {
                    .GeneratorWeight = 3,
                    .Description = "a hilly area"
                }
            },
            {
                TerrainType.Mountains,
                New TerrainTypeDescriptor With
                {
                    .GeneratorWeight = 2,
                    .Description = "a mountainous area"
                }
            },
            {
                TerrainType.Plains,
                New TerrainTypeDescriptor With
                {
                    .GeneratorWeight = 6,
                    .Description = "a flat plains"
                }
            }
        }
End Module