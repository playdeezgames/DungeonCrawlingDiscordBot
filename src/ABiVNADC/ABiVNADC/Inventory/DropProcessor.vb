Module DropProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Drop what?",
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireItemNameQuantity(
                            tokens,
                            AddressOf character.Inventory.FindItemsByName,
                            builder,
                            Sub(items)
                                character.Drop(items, builder)
                            End Sub)
                    End Sub)
            End Sub)

    End Sub
End Module
