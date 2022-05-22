Friend Class TrousersDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("trousers", False)
        SpawnCount = AddressOf RareSpawn
        EquipSlot = EquipSlot.Legs
        Durability = Function(x) If(x = DurabilityType.Armor, 1, 0)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        EquippedEncumbrance = -4
        Aliases = New List(Of String) From {"pants"}
    End Sub
End Class
