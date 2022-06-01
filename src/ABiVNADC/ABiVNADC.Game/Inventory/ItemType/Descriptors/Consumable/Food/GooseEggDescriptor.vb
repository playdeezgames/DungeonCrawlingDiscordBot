Imports System.Text

Friend Class GooseEggDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        Const FoodFatigueRecovery As Long = 2
        builder.AppendLine($"{character.FullName} eats goose egg")
        character.AddFatigue(-FoodFatigueRecovery)
        builder.AppendLine($"{character.FullName} now has {character.Statistic(StatisticType.Energy)} energy.")
        item.Destroy()
    End Sub
    Sub New()
        MyBase.New("goose egg", True, EquipSlot.None)
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"egg"}
    End Sub
End Class
