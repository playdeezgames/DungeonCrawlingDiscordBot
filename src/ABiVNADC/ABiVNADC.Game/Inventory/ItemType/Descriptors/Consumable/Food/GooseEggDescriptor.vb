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
        MyBase.New()
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"egg"}
    End Sub
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "goose egg"
        End Get
    End Property
End Class
