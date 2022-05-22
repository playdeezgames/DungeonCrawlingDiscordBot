Friend Class CompassDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("compass", True)
        UseMessage = Function(x) $"{x} looks at their compass"
        CanBuyGenerator = MakeBooleanGenerator(49, 1)
        BuyPriceDice = "500d1+2d500"
        OnUse = Sub(character, item, builder)
                    builder.AppendLine(ItemType.Compass.UseMessage(character.FullName))
                    builder.AppendLine($"{character.FullName} is facing {character.Player.AheadDirection.Value.Name}")
                End Sub
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"c"}
    End Sub
End Class
