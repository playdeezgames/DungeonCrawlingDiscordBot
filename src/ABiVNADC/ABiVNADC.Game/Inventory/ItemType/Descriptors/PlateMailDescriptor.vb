Friend Class PlateMailDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("plate mail")
        SpawnCount = AddressOf VeryRareSpawn
        EquipSlot = EquipSlot.Body
        DefendDice = Function(x) "1d3/3+1d3/3"
        Durability = Function(x) If(x = DurabilityType.Armor, 35, 0)
        CanBuyGenerator = MakeBooleanGenerator(49, 1)
        BuyPriceDice = "150d1+2d150"
        InventoryEncumbrance = 30
        EquippedEncumbrance = 20
        Aliases = New List(Of String) From {"pm", "plate"}
    End Sub
End Class
