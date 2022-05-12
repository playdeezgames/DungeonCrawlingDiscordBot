Module TakeProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Take what?",
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireLocation(
                            character,
                            builder,
                            Sub(location)
                                If tokens.Count = 1 AndAlso tokens.Single() = AllText Then
                                    HandleTakeAll(player, character, location, builder)
                                    Return
                                End If
                                RequireItemTypeQuantity(
                                    tokens,
                                    builder,
                                    Sub(itemType, quantity)
                                        player.Take(itemType, quantity, builder)
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub

    Private Sub HandleTakeAll(player As Player, character As Character, location As Location, builder As StringBuilder)
        player.TakeAll(builder)
    End Sub
End Module
