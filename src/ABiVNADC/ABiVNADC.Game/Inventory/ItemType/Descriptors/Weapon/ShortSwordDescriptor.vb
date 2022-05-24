Friend Class ShortSwordDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("short sword", False)
        EquipSlot = EquipSlot.Weapon
        AttackDice = Function(x) "1d2/2+1d2/2"
        Durability = Function(x) If(x = DurabilityType.Weapon, 10, 0)
        CanBuyGenerator = MakeBooleanGenerator(9, 1)
        BuyPriceDice = "25d1+2d25"
        InventoryEncumbrance = 6
        EquippedEncumbrance = 4
        Aliases = New List(Of String) From {"ss"}
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) 1},
            {Difficulty.Easy, Function(x) 2},
            {Difficulty.Normal, Function(x) 4},
            {Difficulty.Difficult, Function(x) 6},
            {Difficulty.Too, Function(x) 8}
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
