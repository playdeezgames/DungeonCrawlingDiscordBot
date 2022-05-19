Module TakeProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Take what?",
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        If tokens.Count = 1 AndAlso tokens.Single() = AllText Then
                            HandleTakeAll(player, builder)
                            Return
                        End If
                        If tokens.Count = 1 AndAlso tokens.Single() = TrophiesText Then
                            HandleTakeTrophies(player, builder)
                            Return
                        End If
                        RequireItemNameQuantity(
                            tokens,
                            AddressOf location.Inventory.FindItemsByName,
                            builder,
                            Sub(items)
                                player.Take(items, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub

    Private Sub HandleTakeTrophies(player As Player, builder As StringBuilder)
        player.TakeTrophies(builder)
    End Sub

    Private Sub HandleTakeAll(player As Player, builder As StringBuilder)
        player.TakeAll(builder)
    End Sub
End Module
