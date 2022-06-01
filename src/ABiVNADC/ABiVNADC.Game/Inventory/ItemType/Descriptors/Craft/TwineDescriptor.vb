Friend Class TwineDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New(
            "twine",
            False,
            EquipSlot.None,
            New List(Of Recipe) From
            {
                New Recipe(
                    New Dictionary(Of ItemType, Long) From {{ItemType.PlantFiber, 2}},
                    New Dictionary(Of ItemType, Long) From {{ItemType.Twine, 1}})
            })
    End Sub
End Class
