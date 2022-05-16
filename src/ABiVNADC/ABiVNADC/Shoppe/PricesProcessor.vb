Module PricesProcessor
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
                        Select Case location.LocationType
                            Case LocationType.Shoppe
                                RequireInsideShoppe(
                                    character,
                                    location,
                                    builder,
                                    Sub(shoppe)
                                        builder.AppendLine("Prices:")
                                        For Each buyPrice In shoppe.BuyPrices
                                            builder.AppendLine($"- {buyPrice.Key.Name} : {buyPrice.Value} credits")
                                        Next
                                    End Sub)
                            Case LocationType.LandClaimOffice
                                builder.AppendLine($"A land claim costs {location.LandClaimOffice.ClaimPrice} jools.")
                            Case Else
                                builder.AppendLine("There's nothing to buy here.")
                        End Select
                    End Sub)
            End Sub)
    End Sub
End Module
