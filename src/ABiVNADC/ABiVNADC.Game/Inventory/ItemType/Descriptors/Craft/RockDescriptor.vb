Imports System.Text

Friend Class RockDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.Weapon
        End Get
    End Property

    Public Overrides Function AttackDice(item As Item) As String
        Return "1d3/3"
    End Function

    Public Overrides ReadOnly Property CanThrow As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "rock"
        End Get
    End Property

    Public Overrides Sub OnThrow(character As Character, item As Item, location As Location, builder As StringBuilder)
        Dim enemy = location.Enemies(character).FirstOrDefault
        If enemy Is Nothing Then
            builder.AppendLine($"{character.FullName} throws a rock. It lands on the floor.")
            location.Inventory.Add(item)
            Return
        End If
        builder.AppendLine($"{character.FullName} throws a rock at {enemy.FullName}.")
        Dim highRoll = RNG.RollDice("1d3")
        enemy.ChangeEffectDuration(EffectType.Groggy, highRoll)
        character.Attack(enemy, builder)
        location.Inventory.Add(item)
    End Sub
End Class
