Friend Class TrousersDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        EquippedEncumbrance = -4
        Aliases = New List(Of String) From {"pants"}
    End Sub
    Public Overrides Function Durability(durabilityType As DurabilityType) As Long
        Return If(durabilityType = DurabilityType.Armor, 1, 0)
    End Function
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Legs
        End Get
    End Property
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
        Dim candidates = locations
        Dim count = SpawnCountTable(difficulty)(candidates.LongCount)
        While result.LongCount < count
            result.Add(RNG.FromEnumerable(candidates))
        End While
        Return result
    End Function

    Public Overrides Function GetName() As String
        Return "trousers"
    End Function
End Class
