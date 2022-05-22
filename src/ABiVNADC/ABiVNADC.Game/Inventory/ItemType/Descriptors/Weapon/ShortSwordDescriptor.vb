Friend Class ShortSwordDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("short sword", False)
        SpawnCount = AddressOf RareSpawn
        EquipSlot = EquipSlot.Weapon
        AttackDice = Function(x) "1d2/2+1d2/2"
        Durability = Function(x) If(x = DurabilityType.Weapon, 10, 0)
        CanBuyGenerator = MakeBooleanGenerator(9, 1)
        BuyPriceDice = "25d1+2d25"
        InventoryEncumbrance = 6
        EquippedEncumbrance = 4
        Aliases = New List(Of String) From {"ss"}
    End Sub

End Class
