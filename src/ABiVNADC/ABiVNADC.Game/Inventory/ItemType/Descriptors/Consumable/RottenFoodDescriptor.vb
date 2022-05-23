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
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"rf"}
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) x \ 8},
            {Difficulty.Easy, Function(x) x \ 6},
            {Difficulty.Normal, Function(x) x \ 4},
            {Difficulty.Difficult, Function(x) x \ 3},
            {Difficulty.Too, Function(x) x \ 2}
        }
    Public Overrides Function SpawnLocations(difficulty As Difficulty, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Dim result As New List(Of Location)
        Dim candidates = locations
        Dim count = SpawnCountTable(difficulty)(candidates.LongCount)
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(candidates))
        End While
        Return result
    End Function
End Class
