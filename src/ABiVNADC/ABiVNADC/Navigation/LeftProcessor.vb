Module LeftProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            LeftText,
            builder,
            Sub()
                player.TurnLeft(builder)
                ShowCurrentLocation(player, builder)
            End Sub)
    End Sub
End Module
