Module RightProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            RightText,
            builder,
            Sub()
                player.TurnRight(builder)
                ShowCurrentLocation(player, builder)
            End Sub)
    End Sub
End Module
