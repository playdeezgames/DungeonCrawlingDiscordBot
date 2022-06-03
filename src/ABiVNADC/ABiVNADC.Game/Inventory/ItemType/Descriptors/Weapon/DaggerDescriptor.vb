Imports System.Text

Friend Class DaggerDescriptor
    Inherits ItemTypeDescriptor

    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} commits seppuku")
        character.Destroy(False)
    End Sub
    Sub New()
        MyBase.New()
        BuyPriceDice = "12d1+2d12"
        InventoryEncumbrance = 1
        EquippedEncumbrance = 0
        Aliases = New List(Of String) From {"d"}
    End Sub
    Public Overrides Function GenerateCanBuy() As Boolean
        Return RNG.FromGenerator(MakeBooleanGenerator(4, 1))
    End Function
    Public Overrides Function Durability(durabilityType As DurabilityType) As Long
        Return If(durabilityType = DurabilityType.Weapon, 5, 0)
    End Function
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Weapon
        End Get
    End Property
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides Function AttackDice(item As Item) As String
        Return "1d2/2"
    End Function
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

    Public Overrides ReadOnly Property Name As String
        Get
            Return "dagger"
        End Get
    End Property

    Public Overrides ReadOnly Property CanThrow As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides Sub OnThrow(character As Character, item As Item, location As Location, builder As StringBuilder)
        Dim enemy = location.Enemies(character).FirstOrDefault
        If enemy Is Nothing Then
            location.Inventory.Add(item)
            builder.AppendLine($"{character.FullName} throws the dagger. It falls to the floor.")
            Return
        End If
        Dim attackRoll = character.RollAttack()
        Dim defendRoll = enemy.RollDefend()
        If attackRoll <= defendRoll Then
            location.Inventory.Add(item)
            builder.AppendLine($"{character.FullName} throws the dagger at {enemy.FullName}. It misses falls to the floor.")
            Return
        End If
        enemy.Inventory.Add(item)
        enemy.ChangeEffectDuration(EffectType.Shrapnel, 100)
        builder.AppendLine($"{character.FullName} throws the dagger at {enemy.FullName}. It hits and lodges in their body.")
    End Sub
End Class
