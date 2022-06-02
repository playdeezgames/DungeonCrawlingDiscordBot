Imports System.Text

Friend Class CompassDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(EquipSlot.None)
        CanBuyGenerator = MakeBooleanGenerator(49, 1)
        BuyPriceDice = "500d1+2d500"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"c"}
    End Sub
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} looks at their compass")
        builder.AppendLine($"{character.FullName} is facing {character.Player.AheadDirection.Value.Name}")
    End Sub

    Public Overrides Function GetName() As String
        Return "compass"
    End Function
End Class
