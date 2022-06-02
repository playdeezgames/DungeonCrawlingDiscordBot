Imports System.Text

Friend Class FireScrollDescriptor
    Inherits ItemTypeDescriptor

    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} uses the fire scroll")
        item.Destroy()
        Dim enemies = character.Location.Enemies(character)
        For Each enemy In enemies
            Dim damageRoll = RNG.RollDice("1d2/2")
            enemy.ApplyElementalDamage(ElementalDamageType.Fire, damageRoll, builder)
        Next
    End Sub
    Sub New()
        MyBase.New(EquipSlot.None)
        CanBuyGenerator = MakeBooleanGenerator(3, 1)
        BuyPriceDice = "25d1+2d25"
        InventoryEncumbrance = 0
        Aliases = New List(Of String) From {"scroll"}
    End Sub
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function GetName() As String
        Return "fire scroll"
    End Function
End Class
