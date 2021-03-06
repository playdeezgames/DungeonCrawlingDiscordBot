Imports System.Text

Friend Class StoneSpearDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        InventoryEncumbrance = 2
        EquippedEncumbrance = 1
        Aliases = New List(Of String) From {"spear"}
    End Sub
    Public Overrides Function Durability(durabilityType As DurabilityType) As Long
        Return If(durabilityType = DurabilityType.Weapon, 2, 0)
    End Function
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Weapon
        End Get
    End Property

    Public Overrides ReadOnly Property Recipes As IReadOnlyList(Of Recipe)
        Get
            Return New List(Of Recipe) From
            {
                New Recipe(
                    New Dictionary(Of ItemType, Long) From
                    {
                        {ItemType.Stick, 1},
                        {ItemType.Twine, 1},
                        {ItemType.SharpRock, 1}
                    },
                    New Dictionary(Of ItemType, Long) From {{ItemType.StoneSpear, 1}})
            }
        End Get
    End Property
    Public Overrides Function AttackDice(item As Item) As String
        Return "1d3/3+1d3/3"
    End Function
    Public Overrides ReadOnly Property CanThrow As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "stone spear"
        End Get
    End Property

    Public Overrides Sub OnThrow(character As Character, item As Item, location As Location, builder As StringBuilder)
        Dim enemy = location.Enemies(character).FirstOrDefault
        If enemy Is Nothing Then
            location.Inventory.Add(item)
            builder.AppendLine($"{character.FullName} throws the stone spear. It falls to the floor.")
            Return
        End If
        Dim attackRoll = character.RollAttack()
        Dim defendRoll = enemy.RollDefend()
        If attackRoll <= defendRoll Then
            location.Inventory.Add(item)
            builder.AppendLine($"{character.FullName} throws the stone spear at {enemy.FullName}. It misses falls to the floor.")
            Return
        End If
        enemy.Inventory.Add(item)
        enemy.ChangeEffectDuration(EffectType.Shrapnel, 100)
        builder.AppendLine($"{character.FullName} throws the stone spear at {enemy.FullName}. It hits and lodges in their body.")
    End Sub
End Class
