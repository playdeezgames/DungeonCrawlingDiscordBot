Module SellProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Sell what?",
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
                                        If Not character.Inventory.HasItem(itemType) Then
                                            builder.AppendLine($"{character.FullName} does not have any {itemType.Name} to sell.")
                                            Return
                                        End If
                                        RequireInsideShoppe(
                                            character,
                                            location,
                                            builder,
                                            Sub(shoppe)
                                                Dim credit = shoppe.SellItem(character, itemType)
                                                If credit > 0 Then
                                                    builder.AppendLine($"{character.FullName} sells {itemType.Name} for {credit} credits.")
                                                Else
                                                    builder.AppendLine($"{shoppe.Name} does not buy {itemType.Name}.")
                                                End If
                                            End Sub)
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
