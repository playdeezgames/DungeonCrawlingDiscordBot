Friend Class PlateMailDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("plate mail", False, EquipSlot.Body)
        DefendDice = Function(x) "1d3/3+1d3/3"
        Durability = Function(x) If(x = DurabilityType.Armor, 35, 0)
        CanBuyGenerator = MakeBooleanGenerator(49, 1)
        BuyPriceDice = "150d1+2d150"
        InventoryEncumbrance = 30
        EquippedEncumbrance = 20
        Aliases = New List(Of String) From {"pm", "plate"}
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) 0},
            {Difficulty.Easy, Function(x) 0},
            {Difficulty.Normal, Function(x) 1},
            {Difficulty.Difficult, Function(x) 2},
            {Difficulty.Too, Function(x) 4}
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
End Class
