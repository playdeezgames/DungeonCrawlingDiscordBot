Friend Class RottenFoodDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("rotten food")
        SpawnCount = AddressOf VeryCommonSpawn
        CanUse = True
        UseMessage = Function(x) $"{x} eats rotten food"
        OnUse = Sub(character, item, builder)
                    Const FoodFatigueRecovery As Long = 4
                    builder.AppendLine(ItemType.RottenFood.UseMessage(character.FullName))
                    character.AddFatigue(-FoodFatigueRecovery)
                    If RNG.RollDice("1d2/2") > 0 Then
                        character.ChangeEffectDuration(EffectType.Nausea, RNG.RollDice("2d6"))
                        builder.AppendLine($"{character.FullName} is a little queasy from the tainted food!")
                    End If
                    builder.AppendLine($"{character.FullName} now has {character.Statistic(StatisticType.Energy)} energy.")
                    item.Destroy()
                End Sub
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"rf"}
    End Sub

End Class
