Friend Class LongSwordDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("long sword", False)
        EquipSlot = EquipSlot.Weapon
        AttackDice = Function(x) "1d2/2+1d2/2+1d2/2"
        Durability = Function(x) If(x = DurabilityType.Weapon, 20, 0)
        CanBuyGenerator = MakeBooleanGenerator(49, 1)
        BuyPriceDice = "50d1+2d50"
        InventoryEncumbrance = 10
        EquippedEncumbrance = 5
        Aliases = New List(Of String) From {"ls"}
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) 0},
            {Difficulty.Easy, Function(x) 1},
            {Difficulty.Normal, Function(x) 2},
            {Difficulty.Difficult, Function(x) 4},
            {Difficulty.Too, Function(x) 4}
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
