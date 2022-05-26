Imports System.Text

Friend Class FoodDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        Const FoodFatigueRecovery As Long = 4
        builder.AppendLine($"{character.FullName} eats food")
        character.AddFatigue(-FoodFatigueRecovery)
        builder.AppendLine($"{character.FullName} now has {character.Statistic(StatisticType.Energy)} energy.")
        item.Destroy()
    End Sub
    Sub New()
        MyBase.New("food", True)
        CanBuyGenerator = MakeBooleanGenerator(1, 9)
        BuyPriceDice = "2d1+2d2"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"f"}
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) x \ 2},
            {Difficulty.Easy, Function(x) x \ 3},
            {Difficulty.Normal, Function(x) x \ 4},
            {Difficulty.Difficult, Function(x) x \ 6},
            {Difficulty.Too, Function(x) x \ 8}
        }
    Public Overrides Function SpawnLocations(difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Dim result As New List(Of Location)
        Dim candidates = locations
        Dim count = SpawnCountTable(difficulty)(candidates.LongCount)
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(candidates))
        End While
        Return result
    End Function
End Class
