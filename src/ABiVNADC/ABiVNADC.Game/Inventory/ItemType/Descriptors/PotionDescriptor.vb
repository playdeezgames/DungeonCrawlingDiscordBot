Friend Class PotionDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "potion"
        SpawnCount = AddressOf RareSpawn
        CanUse = True
        UseMessage = Function(x) $"{x} drinks a potion"
        CanBuyGenerator = MakeBooleanGenerator(1, 4)
        BuyPriceDice = "50d1+2d50"
        OnUse = Sub(character, item, builder)
                    Const PotionWoundRecovery As Long = 4
                    builder.AppendLine(ItemType.Potion.UseMessage(character.FullName))
                    character.AddWounds(-PotionWoundRecovery)
                    builder.Append($"{character.FullName} now has {character.Statistic(StatisticType.Health)} health.")
                    item.Destroy()
                End Sub
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"p", "pot"}
    End Sub
End Class
