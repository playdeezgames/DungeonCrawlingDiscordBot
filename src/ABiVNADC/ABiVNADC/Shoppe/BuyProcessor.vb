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
                        RequireItemTypeQuantity(
                            tokens,
                            builder,
                            Sub(itemType, quantity)
                                Select Case location.LocationType
                                    Case LocationType.Shoppe
                                        RequireInsideShoppe(
                                            character,
                                            location,
                                            builder,
                                            Sub(shoppe)
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
                                            End Sub)
                                    Case LocationType.LandClaimOffice
                                        character.BuyLandClaims(quantity, builder)
                                    Case Else
                                        builder.AppendLine("There is nothing here to buy.")
                                End Select
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
