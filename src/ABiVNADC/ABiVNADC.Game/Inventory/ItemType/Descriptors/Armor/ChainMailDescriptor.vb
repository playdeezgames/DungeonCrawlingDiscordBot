Friend Class ChainMailDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        BuyPriceDice = "75d1+2d75"
        InventoryEncumbrance = 20
        EquippedEncumbrance = 15
        Aliases = New List(Of String) From {"cm", "chain"}
    End Sub

    Public Overrides Function GenerateCanBuy() As Boolean
        Return RNG.FromGenerator(MakeBooleanGenerator(19, 1))
    End Function

    Public Overrides Function Durability(durabilityType As DurabilityType) As Long
        Return If(durabilityType = DurabilityType.Armor, 20, 0)
    End Function
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Body
        End Get
    End Property
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

    Public Overrides ReadOnly Property Name As String
        Get
            Return "chain mail"
        End Get
    End Property
End Class
