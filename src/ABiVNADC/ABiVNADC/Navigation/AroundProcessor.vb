Module AroundProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            AroundText,
            builder,
            Sub()
                player.TurnAround(builder)
                ShowCurrentLocation(player, builder)
            End Sub)
    End Sub
End Module
