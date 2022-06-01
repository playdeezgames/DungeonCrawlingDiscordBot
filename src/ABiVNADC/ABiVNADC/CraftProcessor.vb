Module CraftProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            $"{CraftText} (item type name)",
            builder,
            Sub()
                RequireItemTypeQuantity(
                    tokens,
                    builder,
                    Sub(itemType, quantity)
                        player.Craft(itemType, quantity, builder)
                    End Sub)
            End Sub)
    End Sub
End Module
