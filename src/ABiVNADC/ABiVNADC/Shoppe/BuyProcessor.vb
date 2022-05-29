Module BuyProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Buy what?",
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        Select Case location.LocationType
                            Case LocationType.IncentivesOffice
                                RunIncentiveOffice(player, builder, StitchTokens(tokens))
                            Case LocationType.Shoppe
                                RequireItemTypeQuantity(
                                    tokens,
                                    builder,
                                    Sub(itemType, quantity)
                                        RunShoppe(location, itemType, quantity, character, builder)
                                    End Sub)
                            Case LocationType.LandClaimOffice
                                RequireItemTypeQuantity(
                                    tokens,
                                    builder,
                                    Sub(itemType, quantity)
                                        If itemType <> ItemType.LandClaim Then
                                            builder.AppendLine("This is a land claim office. We sell LAND CLAIMS here!")
                                            Return
                                        End If
                                        character.BuyLandClaims(quantity, builder)
                                    End Sub)
                            Case Else
                                builder.AppendLine("There is nothing here to buy.")
                        End Select
                    End Sub)
            End Sub)
    End Sub
    Private Sub RunIncentiveOffice(player As Player, builder As StringBuilder, incentiveName As String)
        Dim incentiveType = AllIncentives.SingleOrDefault(Function(x) x.Name = incentiveName)
        If incentiveType = IncentiveType.None Then
            builder.AppendLine($"There is no incentive named `{incentiveName}`.")
            Return
        End If
        player.BuyIncentive(incentiveType, builder)
    End Sub
    Private Sub RunShoppe(location As Location, itemType As ItemType, quantity As Long, character As Character, builder As StringBuilder)
        Dim shoppe = location.Shoppe
        If Not shoppe.CanBuy(itemType) Then
            builder.AppendLine($"{shoppe.Name} does not sell {itemType.Name}.")
            Return
        End If
        Dim cost = shoppe.BuyPrices(itemType) * quantity
        If shoppe.CreditBalance(character) < cost Then
            builder.AppendLine($"{character.FullName} does not have enough credit to buy {itemType.Name}.")
            Return
        End If
        builder.AppendLine($"{character.FullName} buys {quantity} {itemType.Name} for {cost} credits.")
        While quantity > 0
            quantity -= 1
            shoppe.BuyItem(character, itemType)
        End While
    End Sub
End Module
