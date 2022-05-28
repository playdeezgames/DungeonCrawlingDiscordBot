Module IncentivesProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            IncentivesText,
            builder,
            Sub()
                builder.AppendLine($"You have {player.IncentivePoints} incentive points.")
            End Sub)
    End Sub
End Module
