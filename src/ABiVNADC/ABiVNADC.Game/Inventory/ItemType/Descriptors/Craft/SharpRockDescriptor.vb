Friend Class SharpRockDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New(
            "sharp rock",
            False,
            EquipSlot.None,
            New List(Of Recipe) From
            {
                New Recipe(
                    New Dictionary(Of ItemType, Long) From {{ItemType.Rock, 2}},
                    New Dictionary(Of ItemType, Long) From
                    {
                        {ItemType.Rock, 1},
                        {ItemType.SharpRock, 1}
                    })
            })
    End Sub
End Class
