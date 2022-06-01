Imports System.Text

Friend Class SleepScrollDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} uses the sleep scroll")
        item.Destroy()
        Dim enemies = character.Location.Enemies(character)
        For Each enemy In enemies
            Dim maximumFatigue = enemy.Maximum(StatisticType.Energy)
            If maximumFatigue > 0 Then
                enemy.AddFatigue(maximumFatigue)
                enemy.ChangeEffectDuration(EffectType.Groggy, 1)
                builder.AppendLine($"{enemy.FullName} is hit by a wave of drowsiness!")
            End If
        Next
    End Sub
    Sub New()
        MyBase.New("sleep scroll", True, EquipSlot.None)
        CanBuyGenerator = MakeBooleanGenerator(3, 1)
        BuyPriceDice = "25d1+2d25"
        InventoryEncumbrance = 0
        Aliases = New List(Of String) From {"scroll"}
    End Sub
End Class
