Module ForageProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            ForageText,
            builder,
            Sub()
                player.Forage(builder)
            End Sub)
    End Sub
End Module
