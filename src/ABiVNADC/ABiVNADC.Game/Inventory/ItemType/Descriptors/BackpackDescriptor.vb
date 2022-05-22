Friend Class BackpackDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "backpack"
        SpawnCount = AddressOf VeryRareSpawn
        EquipSlot = EquipSlot.Back
        Durability = Function(x) If(x = DurabilityType.Armor, 10, 0)
        CanBuyGenerator = MakeBooleanGenerator(9, 1)
        BuyPriceDice = "200d1+2d200"
        InventoryEncumbrance = 1
        EquippedEncumbrance = -20
        Aliases = New List(Of String) From {"pack"}
    End Sub

End Class
