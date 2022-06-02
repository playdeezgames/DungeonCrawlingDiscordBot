Friend Class ChainMailDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(EquipSlot.Body)
        Durability = Function(x) If(x = DurabilityType.Armor, 20, 0)
        CanBuyGenerator = MakeBooleanGenerator(19, 1)
        BuyPriceDice = "75d1+2d75"
        InventoryEncumbrance = 20
        EquippedEncumbrance = 15
        Aliases = New List(Of String) From {"cm", "chain"}
    End Sub
    Public Overrides Function DefendDice(item As Item) As String
        Return "1d3/3"
    End Function
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) 0},
            {Difficulty.Easy, Function(x) 1},
            {Difficulty.Normal, Function(x) 2},
            {Difficulty.Difficult, Function(x) 4},
            {Difficulty.Too, Function(x) 6}
        }
    Public Overrides Function SpawnLocations(difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Dim result As New List(Of Location)
        Dim candidates = locations.Where(Function(x) x.RouteCount = 1)
        Dim count = SpawnCountTable(difficulty)(candidates.LongCount)
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(candidates))
        End While
        Return result
    End Function

    Public Overrides Function GetName() As String
        Return "chain mail"
    End Function
End Class
