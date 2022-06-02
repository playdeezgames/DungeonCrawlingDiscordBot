﻿Friend Class HelmetDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        Durability = Function(x) If(x = DurabilityType.Armor, 5, 0)
        CanBuyGenerator = MakeBooleanGenerator(4, 1)
        BuyPriceDice = "12d1+2d12"
        InventoryEncumbrance = 5
        EquippedEncumbrance = 3
        Aliases = New List(Of String) From {"h", "helm"}
    End Sub
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Head
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

    Public Overrides Function GetName() As String
        Return "helmet"
    End Function
End Class
