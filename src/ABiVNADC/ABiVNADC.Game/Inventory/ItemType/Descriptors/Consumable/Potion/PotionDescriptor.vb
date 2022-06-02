Imports System.Text

Friend Class PotionDescriptor
    Inherits ItemTypeDescriptor

    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        Const PotionWoundRecovery As Long = 4
        builder.AppendLine($"{character.FullName} drinks a potion")
        character.AddWounds(-PotionWoundRecovery)
        builder.AppendLine($"{character.FullName} now has {character.Statistic(StatisticType.Health)} health.")
        item.Destroy()
    End Sub
    Sub New()
        MyBase.New()
        CanBuyGenerator = MakeBooleanGenerator(1, 4)
        BuyPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"p", "pot"}
    End Sub
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

    Public Overrides Function GetName() As String
        Return "potion"
    End Function
End Class
