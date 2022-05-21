Module RestProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            RestText,
            builder,
            Sub()
                player.Rest(builder)
            End Sub)
    End Sub
End Module
