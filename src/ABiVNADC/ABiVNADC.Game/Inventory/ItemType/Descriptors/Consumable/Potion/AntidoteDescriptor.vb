Imports System.Text

Friend Class AntidoteDescriptor
    Inherits ItemTypeDescriptor

    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} drinks the antidote!")
        character.PurgePoisons()
        item.Destroy()
    End Sub
    Sub New()
        MyBase.New()
        BuyPriceDice = "5d1+2d5"
        InventoryEncumbrance = 1
        Aliases = New List(Of String)
    End Sub
    Public Overrides Function GenerateCanBuy() As Boolean
        Return RNG.FromGenerator(MakeBooleanGenerator(1, 1))
    End Function
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property
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

    Public Overrides ReadOnly Property Name As String
        Get
            Return "antidote"
        End Get
    End Property
End Class
