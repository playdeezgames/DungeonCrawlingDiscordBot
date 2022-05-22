Friend Class ChainMailDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "chain mail"
        SpawnCount = AddressOf RareSpawn
        EquipSlot = EquipSlot.Body
        DefendDice = Function(x) "1d3/3"
        Durability = Function(x) If(x = DurabilityType.Armor, 20, 0)
        CanBuyGenerator = MakeBooleanGenerator(19, 1)
        BuyPriceDice = "75d1+2d75"
        InventoryEncumbrance = 20
        EquippedEncumbrance = 15
        Aliases = New List(Of String) From {"cm", "chain"}
    End Sub
End Class
