Imports System.Text

Friend Class CompassDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        BuyPriceDice = "500d1+2d500"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"c"}
    End Sub
    Public Overrides Function GenerateCanBuy() As Boolean
        Return RNG.FromGenerator(MakeBooleanGenerator(49, 1))
    End Function
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} looks at their compass")
        builder.AppendLine($"{character.FullName} is facing {character.Player.AheadDirection.Value.Name}")
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "compass"
        End Get
    End Property
End Class
