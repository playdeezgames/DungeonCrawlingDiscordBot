Friend Class LongSwordDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("long sword", False)
        SpawnCount = AddressOf VeryRareSpawn
        EquipSlot = EquipSlot.Weapon
        AttackDice = Function(x) "1d2/2+1d2/2+1d2/2"
        Durability = Function(x) If(x = DurabilityType.Weapon, 20, 0)
        CanBuyGenerator = MakeBooleanGenerator(49, 1)
        BuyPriceDice = "50d1+2d50"
        InventoryEncumbrance = 10
        EquippedEncumbrance = 5
        Aliases = New List(Of String) From {"ls"}
    End Sub
End Class
