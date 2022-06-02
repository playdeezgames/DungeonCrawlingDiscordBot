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
    End Sub

    Public Overrides Function AttackDice(item As Item) As String
        Return "1d2/2"
    End Function

    Public Overrides Function GetName() As String
        Return "sharp rock"
    End Function
End Class
