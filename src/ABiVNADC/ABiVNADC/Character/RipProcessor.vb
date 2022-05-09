Module RipProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            RipText,
            builder,
            Sub()
                Dim rips = player.RIPs
                If Not rips.any Then
                    builder.AppendLine("None have fallen... yet!")
                Else
                    builder.Append("The name(s) of the fallen: ")
                    builder.AppendJoin(", ", rips)
                    builder.AppendLine()
                End If
            End Sub)
    End Sub
End Module
