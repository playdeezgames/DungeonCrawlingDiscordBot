Imports System.Text

Friend Class StickDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(False, EquipSlot.Weapon)
        AttackDice = Function(x) "1d3/2"
    End Sub
    Public Overrides ReadOnly Property CanThrow As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function GetName() As String
        Return "stick"
    End Function

    Public Overrides Sub OnThrow(character As Character, item As Item, location As Location, builder As StringBuilder)
        CharacterEquipSlotData.ClearForItem(item.Id)
        location.Inventory.Add(item)
        builder.AppendLine($"{character.FullName} throws the stick. It falls onto the floor.")
    End Sub
End Class
