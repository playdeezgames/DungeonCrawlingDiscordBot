Module BribeProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Bribe with what? Empty handed bribes are rude!",
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        RequireItemName(
                            tokens,
                            AddressOf character.Inventory.FindItemsByName,
                            builder,
                            Sub(item)
                                Dim enemy As Character = character.Location.Enemies(character).FirstOrDefault(Function(x) x.TakesBribe(item))
                                If enemy Is Nothing Then
                                    builder.AppendLine($"No enemy in this location will take that.")
                                    Return
                                End If
                                player.BribeEnemy(enemy, item, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
