Imports System.Text

Friend Class DaggerDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} commits seppuku")
        character.Destroy()
    End Sub
    Sub New()
        MyBase.New("dagger", True)
        SpawnCount = AddressOf UncommonSpawn
        EquipSlot = EquipSlot.Weapon
        AttackDice = Function(x) "1d2/2"
        Durability = Function(x) If(x = DurabilityType.Weapon, 5, 0)
        CanBuyGenerator = MakeBooleanGenerator(4, 1)
        BuyPriceDice = "12d1+2d12"
        InventoryEncumbrance = 1
        EquippedEncumbrance = 0
        Aliases = New List(Of String) From {"d"}
    End Sub
End Class
