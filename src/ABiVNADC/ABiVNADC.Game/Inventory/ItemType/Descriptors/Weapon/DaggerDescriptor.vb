Imports System.Text

Friend Class DaggerDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} commits seppuku")
        character.Destroy(False)
    End Sub
    Sub New()
        MyBase.New("dagger", True)
        EquipSlot = EquipSlot.Weapon
        AttackDice = Function(x) "1d2/2"
        Durability = Function(x) If(x = DurabilityType.Weapon, 5, 0)
        CanBuyGenerator = MakeBooleanGenerator(4, 1)
        BuyPriceDice = "12d1+2d12"
        InventoryEncumbrance = 1
        EquippedEncumbrance = 0
        Aliases = New List(Of String) From {"d"}
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) 4},
            {Difficulty.Easy, Function(x) 6},
            {Difficulty.Normal, Function(x) 8},
            {Difficulty.Difficult, Function(x) 8},
            {Difficulty.Too, Function(x) 12}
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
