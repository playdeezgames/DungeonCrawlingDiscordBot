Imports System.Text

Friend Class RottenFoodDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        Const FoodFatigueRecovery As Long = 4
        builder.AppendLine($"{character.FullName} eats rotten food")
        character.AddFatigue(-FoodFatigueRecovery)
        If RNG.RollDice("1d2/2") > 0 Then
            character.ChangeEffectDuration(EffectType.Nausea, RNG.RollDice("2d6"))
            builder.AppendLine($"{character.FullName} is a little queasy from the tainted food!")
        End If
        builder.AppendLine($"{character.FullName} now has {character.Statistic(StatisticType.Energy)} energy.")
        item.Destroy()
    End Sub
    Sub New()
        MyBase.New("rotten food", True)
        SpawnCount = AddressOf VeryCommonSpawn
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"rf"}
    End Sub

End Class
