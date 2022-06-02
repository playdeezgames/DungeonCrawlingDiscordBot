Friend Class SharpRockDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(
            EquipSlot.Weapon)
    End Sub
    Public Overrides ReadOnly Property Recipes As IReadOnlyList(Of Recipe)
        Get
            Return New List(Of Recipe) From
            {
                New Recipe(
                    New Dictionary(Of ItemType, Long) From {{ItemType.Rock, 2}},
                    New Dictionary(Of ItemType, Long) From
                    {
                        {ItemType.Rock, 1},
                        {ItemType.SharpRock, 1}
                    })
            }
        End Get
    End Property
    Public Overrides Function AttackDice(item As Item) As String
        Return "1d2/2"
    End Function

    Public Overrides Function GetName() As String
        Return "sharp rock"
    End Function
End Class
