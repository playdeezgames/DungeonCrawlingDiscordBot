Imports System.Text

Friend Class GoosePoopDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"poop"}
        IsTrophy = True
    End Sub
    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Weapon
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "goose poop"
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
            builder.AppendLine($"{character.FullName} throws the goose poop, and it hits the floor.")
            Return
        End If
        item.Destroy()
        enemy.ChangeEffectDuration(EffectType.Nausea, RNG.RollDice("2d3"))
        If enemy.HasEffect(EffectType.Nausea) Then
            builder.AppendLine($"{character.FullName} hits {enemy.FullName} with the goose poop. {enemy.FullName} is disgusted.")
            Return
        End If
        builder.AppendLine($"{character.FullName} hits {enemy.FullName} with the goose poop to no effect.")
    End Sub
End Class
