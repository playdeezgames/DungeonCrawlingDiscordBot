Friend Class ShieldDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanBuyGenerator = MakeBooleanGenerator(9, 1)
        BuyPriceDice = "25d1+2d25"
                         InventoryEncumbrance = 10
                         EquippedEncumbrance = 7
                         Aliases = New List(Of String) From {"sh"}
    End Sub

    Public Overrides Function Durability(durabilityType As DurabilityType) As Long
        Return If(durabilityType = DurabilityType.Armor, 10, 0)
    End Function

    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Shield
        End Get
    End Property
    Public Overrides Function DefendDice(item As Item) As String
        Return "1d3/3"
    End Function
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
        Dim candidates = locations.Where(Function(x) x.RouteCount > 1)
        Dim count = SpawnCountTable(difficulty)(candidates.LongCount)
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(candidates))
        End While
        Return result
    End Function

    Public Overrides ReadOnly Property Name As String
        Get
            Return "shield"
        End Get
    End Property
End Class
