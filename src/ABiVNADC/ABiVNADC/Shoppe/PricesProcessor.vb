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
                                RunShoppe(builder, location)
                            Case LocationType.IncentivesOffice
                                RunIncentiveOffice(player, builder)
                            Case LocationType.LandClaimOffice
                                builder.AppendLine($"A land claim costs {location.LandClaimOffice.ClaimPrice} jools.")
                            Case Else
                                builder.AppendLine("There's nothing to buy here.")
                        End Select
                    End Sub)
            End Sub)
    End Sub

    Private Sub RunShoppe(builder As StringBuilder, location As Location)
        Dim shoppe = location.Shoppe
        builder.AppendLine("Prices:")
        For Each buyPrice In shoppe.BuyPrices
            builder.AppendLine($"- {buyPrice.Key.Name} : {buyPrice.Value} credits")
        Next
    End Sub

    Private Sub RunIncentiveOffice(player As Player, builder As StringBuilder)
        builder.AppendLine("Incentives:")
        For Each incentiveType In AllIncentives
            Dim level = If(player.Incentives.ContainsKey(incentiveType), player.Incentives(incentiveType), 0)
            builder.AppendLine($"- {incentiveType.Name} (cost: {incentiveType.IncentivePrice(level)})")
        Next
    End Sub
End Module
