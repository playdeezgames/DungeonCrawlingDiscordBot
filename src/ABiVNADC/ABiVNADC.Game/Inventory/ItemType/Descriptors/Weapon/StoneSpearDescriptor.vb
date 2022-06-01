Friend Class StoneSpearDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(
            False,
            EquipSlot.Weapon,
            New List(Of Recipe) From
            {
                New Recipe(
                    New Dictionary(Of ItemType, Long) From
                    {
                        {ItemType.Stick, 1},
                        {ItemType.Twine, 1},
                        {ItemType.SharpRock, 1}
                    },
                    New Dictionary(Of ItemType, Long) From {{ItemType.StoneSpear, 1}})
            })
        AttackDice = Function(x) "1d3/3+1d3/3"
        Durability = Function(x) If(x = DurabilityType.Weapon, 2, 0)
        InventoryEncumbrance = 2
        EquippedEncumbrance = 1
        Aliases = New List(Of String) From {"spear"}
    End Sub
    Public Overrides ReadOnly Property CanThrow As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function GetName() As String
        Throw New NotImplementedException()
    End Function
End Class
