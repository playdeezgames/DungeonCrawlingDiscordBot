Friend Class SharpRockDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(
            False,
            EquipSlot.Weapon,
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
        AttackDice = Function(x) "1d2/2"
    End Sub

    Public Overrides Function GetName() As String
        Return "sharp rock"
    End Function
End Class
