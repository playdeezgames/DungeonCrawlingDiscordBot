Imports System.Text

Friend Class AntidoteDescriptor
    Inherits ItemTypeDescriptor

    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} drinks the antidote!")
        character.PurgePoisons()
        item.Destroy()
    End Sub
    Sub New()
        MyBase.New(True, EquipSlot.None)
        CanBuyGenerator = MakeBooleanGenerator(1, 1)
        BuyPriceDice = "5d1+2d5"
        InventoryEncumbrance = 1
        Aliases = New List(Of String)
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) x \ 4},
            {Difficulty.Easy, Function(x) x \ 6},
            {Difficulty.Normal, Function(x) x \ 6},
            {Difficulty.Difficult, Function(x) x \ 8},
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

    Public Overrides Function GetName() As String
        Return "antidote"
    End Function
End Class
