Public Class ItemTypeDescriptor
    Property Name As String

End Class
Module ItemTypeDescriptorExtensions
    Friend ReadOnly ItemTypeDescriptors As New Dictionary(Of ItemType, ItemTypeDescriptor) From
        {
            {
                ItemType.ChainMail,
                New ItemTypeDescriptor With {.Name = "chain mail"}
            },
            {
                ItemType.Dagger,
                New ItemTypeDescriptor With {.Name = "dagger"}
            },
            {
                ItemType.FishFin,
                New ItemTypeDescriptor With {.Name = "fish fin"}
            },
            {
                ItemType.FishScale,
                New ItemTypeDescriptor With {.Name = "fish scale"}
            },
            {
                ItemType.Food,
                New ItemTypeDescriptor With {.Name = "food"}
            },
            {
                ItemType.GoblinEar,
                New ItemTypeDescriptor With {.Name = "goblin ear"}
            },
            {
                ItemType.Helmet,
                New ItemTypeDescriptor With {.Name = "helmet"}
            },
            {
                ItemType.Jools,
                New ItemTypeDescriptor With {.Name = "jools"}
            },
            {
                ItemType.LongSword,
                New ItemTypeDescriptor With {.Name = "long sword"}
            },
            {
                ItemType.OrcTooth,
                New ItemTypeDescriptor With {.Name = "orc tooth"}
            },
            {
                ItemType.PlateMail,
                New ItemTypeDescriptor With {.Name = "plate mail"}
            },
            {
                ItemType.Potion,
                New ItemTypeDescriptor With {.Name = "potion"}
            },
            {
                ItemType.Shield,
                New ItemTypeDescriptor With {.Name = "shield"}
            },
            {
                ItemType.ShortSword,
                New ItemTypeDescriptor With {.Name = "short sword"}
            },
            {
                ItemType.SkullFragment,
                New ItemTypeDescriptor With {.Name = "skull fragment"}
            },
            {
                ItemType.Trousers,
                New ItemTypeDescriptor With {.Name = "trousers"}
            },
            {
                ItemType.ZombieTaint,
                New ItemTypeDescriptor With {.Name = "zombie taint"}
            }
        }
End Module
