Friend Class FoodDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Function UseMessage(name As String) As String
        Return $"{name} eats food"
    End Function
    Sub New()
        MyBase.New("food", True)
        SpawnCount = AddressOf CommonSpawn
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
