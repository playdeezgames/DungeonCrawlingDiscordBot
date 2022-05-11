Module CreditProcessor
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
                                        builder.AppendLine($"{character.FullName} has a credit balance of {shoppe.CreditBalance(character)}.")
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
