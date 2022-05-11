Module PricesProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            PricesText,
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
                                RequireInsideShoppe(
                                    character,
                                    location,
                                    builder,
                                    Sub(shoppe)
                                        builder.AppendLine("Prices:")
                                        For Each buyPrice In shoppe.BuyPrices
                                            builder.AppendLine($"{buyPrice.Key.Name} : {buyPrice.Value} credits")
                                        Next
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
