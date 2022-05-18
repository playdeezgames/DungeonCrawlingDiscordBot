Module OffersProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            PricesText,
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        RequireInsideShoppe(
                            character,
                            location,
                            builder,
                            Sub(shoppe)
                                builder.AppendLine("Offers:")
                                For Each sellPrice In shoppe.SellPrices
                                    builder.AppendLine($"- {sellPrice.Key.Name} : {sellPrice.Value} credits")
                                Next
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
