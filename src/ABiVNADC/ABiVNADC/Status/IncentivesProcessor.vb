Module IncentivesProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            IncentivesText,
            builder,
            Sub()
                builder.AppendLine($"You have {player.IncentivePoints} incentive points.")
                Dim incentives = player.Incentives
                If incentives.any Then
                    builder.AppendLine("Current Incentives:")
                    For Each incentive In incentives
                        builder.AppendLine($"- {incentive.Key.Name} (level {incentive.Value })")
                    Next
                End If
            End Sub)
    End Sub
End Module
