Friend Class HelmetDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("helmet")
        SpawnCount = AddressOf UncommonSpawn
        EquipSlot = EquipSlot.Head
        DefendDice = Function(x) "1d3/3"
        Durability = Function(x) If(x = DurabilityType.Armor, 5, 0)
        CanBuyGenerator = MakeBooleanGenerator(4, 1)
        BuyPriceDice = "12d1+2d12"
        InventoryEncumbrance = 5
        EquippedEncumbrance = 3
        Aliases = New List(Of String) From {"h", "helm"}
    End Sub
End Class
