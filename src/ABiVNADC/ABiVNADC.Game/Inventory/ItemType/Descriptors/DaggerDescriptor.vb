Friend Class DaggerDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "dagger"
        SpawnCount = AddressOf UncommonSpawn
        CanUse = True
        UseMessage = Function(x) $"{x} commits seppuku"
        EquipSlot = EquipSlot.Weapon
        AttackDice = Function(x) "1d2/2"
        Durability = Function(x) If(x = DurabilityType.Weapon, 5, 0)
        CanBuyGenerator = MakeBooleanGenerator(4, 1)
        BuyPriceDice = "12d1+2d12"
        OnUse = Sub(character, item, builder)
                    character.Destroy()
                    builder.AppendLine(ItemType.Dagger.UseMessage(character.FullName))
                End Sub
        InventoryEncumbrance = 1
        EquippedEncumbrance = 0
        Aliases = New List(Of String) From {"d"}
    End Sub
End Class
