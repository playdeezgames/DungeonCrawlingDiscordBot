Imports System.Text

Friend Class AmuletDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New()
        CanBuyGenerator = MakeBooleanGenerator(19, 1)
        BuyPriceDice = "200d1+2d200"
        CanSellGenerator = MakeBooleanGenerator(4, 1)
        SellPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        EquippedEncumbrance = 0
        PostCreate = Sub(item)
                         Dim modifierTable As New Dictionary(Of ModifierType, Integer) From
                            {
                            {ModifierType.None, 16},
                            {ModifierType.Defend, 8},
                            {ModifierType.Energy, 4},
                            {ModifierType.Attack, 2},
                            {ModifierType.Health, 1}
                            }
                         Dim modifier = RNG.FromGenerator(modifierTable)
                         If modifier <> ModifierType.None Then
                             item.AddModifier(modifier, 1)
                         End If
                     End Sub
    End Sub
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Neck
        End Get
    End Property
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        If Not item.Modifiers.Any Then
            builder.AppendLine($"{item.FullName} has no power to confer.")
            Return
        End If
        If Not character.Equipment.Any Then
            builder.AppendLine($"{character.FullName} has no equipment to confer power to.")
        End If
        Dim modifier = RNG.FromEnumerable(item.Modifiers.Where(Function(x) x.Value > 0).Select(Function(x) x.Key))
        Dim target = RNG.FromEnumerable(character.EquipmentItems)
        builder.AppendLine($"{character.FullName} confers {modifier.Name} to {target.FullName}.")
        item.AddModifier(modifier, -1)
        target.AddModifier(modifier, 1)
    End Sub
    Private ReadOnly SpawnCountTable As New Dictionary(Of Difficulty, Func(Of Long, Long)) From
        {
            {Difficulty.Yermom, Function(x) 0},
            {Difficulty.Easy, Function(x) 0},
            {Difficulty.Normal, Function(x) 1},
            {Difficulty.Difficult, Function(x) 2},
            {Difficulty.Too, Function(x) 4}
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
            Return "amulet"
        End Get
    End Property
End Class
