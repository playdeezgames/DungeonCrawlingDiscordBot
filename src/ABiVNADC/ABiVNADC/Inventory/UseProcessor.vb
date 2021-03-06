Module UseProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Use what?",
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireItemName(
                            tokens,
                            AddressOf character.Inventory.FindItemsByName,
                            builder,
                            Sub(item)
                                player.UseItem(item, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
