Imports System.Text

Friend Class FoodDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        Const FoodFatigueRecovery As Long = 4
        builder.AppendLine($"{character.FullName} eats food")
        character.AddFatigue(-FoodFatigueRecovery)
        builder.Append($"{character.FullName} now has {character.Statistic(StatisticType.Energy)} energy.")
        item.Destroy()
    End Sub
    Sub New()
        MyBase.New("food", True)
        SpawnCount = AddressOf CommonSpawn
        CanBuyGenerator = MakeBooleanGenerator(1, 9)
        BuyPriceDice = "2d1+2d2"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"f"}
    End Sub
End Class
