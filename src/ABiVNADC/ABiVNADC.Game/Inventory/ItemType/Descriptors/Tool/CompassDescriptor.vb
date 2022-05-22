﻿Imports System.Text

Friend Class CompassDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Function UseMessage(name As String) As String
        Return $"{name} looks at their compass"
    End Function
    Sub New()
        MyBase.New("compass", True)
        CanBuyGenerator = MakeBooleanGenerator(49, 1)
        BuyPriceDice = "500d1+2d500"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"c"}
    End Sub
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine(ItemType.Compass.UseMessage(character.FullName))
        builder.AppendLine($"{character.FullName} is facing {character.Player.AheadDirection.Value.Name}")
    End Sub
End Class
