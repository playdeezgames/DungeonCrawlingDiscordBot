Module BuyProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Buy what?",
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
                                RequireItemType(
                                    tokens,
                                    builder,
                                    Sub(itemType)
                                        RequireInsideShoppe(
                                            character,
                                            location,
                                            builder,
                                            Sub(shoppe)
                                                If Not shoppe.CanBuy(itemType) Then
                                                    builder.AppendLine($"{shoppe.Name} does not sell {itemType.Name}.")
                                                    Return
                                                End If
                                                Dim cost = shoppe.BuyPrices(itemType)
                                                If shoppe.CreditBalance(character) < cost Then
                                                    builder.AppendLine($"{character.FullName} does not have enough credit to buy {itemType.Name}.")
                                                    Return
                                                End If
                                                shoppe.BuyItem(character, itemType)
                                                builder.AppendLine($"{character.FullName} buys {itemType.Name} for {cost} credits.")
                                            End Sub)
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
