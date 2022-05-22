Friend Class FoodDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "food"
        SpawnCount = AddressOf CommonSpawn
        CanUse = True
        UseMessage = Function(x) $"{x} eats food"
        CanBuyGenerator = MakeBooleanGenerator(1, 9)
        BuyPriceDice = "2d1+2d2"
        OnUse = Sub(character, item, builder)
                    Const FoodFatigueRecovery As Long = 4
                    builder.AppendLine(ItemType.Food.UseMessage(character.FullName))
                    character.AddFatigue(-FoodFatigueRecovery)
                    builder.Append($"{character.FullName} now has {character.Statistic(StatisticType.Energy)} energy.")
                    item.Destroy()
                End Sub
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"f"}
    End Sub

End Class
