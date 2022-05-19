Module DropProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Drop what?",
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        RequireItemNameQuantity(
                            tokens,
                            AddressOf character.Inventory.FindItemsByName,
                            builder,
                            Sub(items)
                                For Each item In items
                                    location.Inventory.Add(item)
                                Next
                                builder.AppendLine($"{character.FullName} drops {items.Count} items.")
                            End Sub)
                    End Sub)
            End Sub)

    End Sub
End Module
