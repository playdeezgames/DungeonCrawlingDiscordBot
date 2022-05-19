Module CreditProcessor
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
                        Dim shoppe = location.Shoppe
                        builder.AppendLine($"{character.FullName} has a credit balance of {shoppe.CreditBalance(character)}.")
                    End Sub)
            End Sub)
    End Sub
End Module
