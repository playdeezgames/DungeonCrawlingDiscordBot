Friend Class LongSwordDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        BuyPriceDice = "50d1+2d50"
        InventoryEncumbrance = 10
        EquippedEncumbrance = 5
        Aliases = New List(Of String) From {"ls"}
    End Sub
    Public Overrides Function GenerateCanBuy() As Boolean
        Return RNG.FromGenerator(MakeBooleanGenerator(49, 1))
    End Function
    Public Overrides Function Durability(durabilityType As DurabilityType) As Long
        Return If(durabilityType = DurabilityType.Weapon, 20, 0)
    End Function
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Weapon
        End Get
    End Property
    Public Overrides Function AttackDice(item As Item) As String
        Return "1d2/2+1d2/2+1d2/2"
    End Function
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) 0},
            {Difficulty.Easy, Function(x) 1},
            {Difficulty.Normal, Function(x) 2},
            {Difficulty.Difficult, Function(x) 4},
            {Difficulty.Too, Function(x) 4}
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

    Public Overrides ReadOnly Property Name As String
        Get
            Return "long sword"
        End Get
    End Property
End Class
