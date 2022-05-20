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
                                If Not item.CanUse Then
                                    builder.AppendLine($"Cannot use `{item.FullName}`.")
                                    Return
                                End If
                                player.UseItem(item, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
