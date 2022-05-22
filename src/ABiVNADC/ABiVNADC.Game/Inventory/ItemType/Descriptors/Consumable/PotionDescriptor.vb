Imports System.Text

Friend Class PotionDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Function UseMessage(name As String) As String
        Return $"{name} drinks a potion"
    End Function
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        Const PotionWoundRecovery As Long = 4
        builder.AppendLine(ItemType.Potion.UseMessage(character.FullName))
        character.AddWounds(-PotionWoundRecovery)
        builder.Append($"{character.FullName} now has {character.Statistic(StatisticType.Health)} health.")
        item.Destroy()
    End Sub
    Sub New()
        MyBase.New("potion", True)
        SpawnCount = AddressOf RareSpawn
        CanBuyGenerator = MakeBooleanGenerator(1, 4)
        BuyPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"p", "pot"}
    End Sub
End Class
