Friend Class ShieldDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("shield", False)
        SpawnCount = AddressOf UncommonSpawn
        EquipSlot = EquipSlot.Shield
        DefendDice = Function(x) "1d3/3"
        Durability = Function(x) If(x = DurabilityType.Armor, 10, 0)
        CanBuyGenerator = MakeBooleanGenerator(9, 1)
        BuyPriceDice = "25d1+2d25"
        InventoryEncumbrance = 10
        EquippedEncumbrance = 7
        Aliases = New List(Of String) From {"sh"}
    End Sub
End Class
