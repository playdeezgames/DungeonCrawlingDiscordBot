Imports System.Text

Friend Class RockDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(EquipSlot.Weapon)
    End Sub

    Public Overrides Function AttackDice(item As Item) As String
        Return "1d3/3"
    End Function

    Public Overrides ReadOnly Property CanThrow As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function GetName() As String
        Return "rock"
    End Function

    Public Overrides Sub OnThrow(character As Character, item As Item, location As Location, builder As StringBuilder)
        Dim enemy = location.Enemies(character).FirstOrDefault
        If enemy Is Nothing Then
            builder.AppendLine($"{character.FullName} throws a rock. It lands on the floor.")
            CharacterEquipSlotData.ClearForItem(item.Id)
            location.Inventory.Add(item)
            Return
        End If
        builder.AppendLine($"{character.FullName} throws a rock at {enemy.FullName}.")
        character.Attack(enemy, builder)
        location.Inventory.Add(item)
        CharacterEquipSlotData.ClearForItem(item.Id)
    End Sub
End Class
