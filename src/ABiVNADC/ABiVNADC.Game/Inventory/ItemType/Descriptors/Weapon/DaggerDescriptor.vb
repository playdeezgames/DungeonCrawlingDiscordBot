Imports System.Text

Friend Class DaggerDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Function UseMessage(name As String) As String
        Return $"{name} commits seppuku"
    End Function
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        character.Destroy()
        builder.AppendLine(ItemType.Dagger.UseMessage(character.FullName))
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
