Module FightProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            FightText,
            builder,
            Sub()
                player.Fight(builder)
            End Sub)
    End Sub
End Module
