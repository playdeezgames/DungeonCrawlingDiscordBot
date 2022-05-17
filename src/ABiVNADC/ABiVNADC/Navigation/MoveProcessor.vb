Module MoveProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            MoveText,
            builder,
            Sub()
                If Not player.CanMove Then
                    builder.AppendLine("You cannot do that now!")
                    If If(player.Character?.IsEncumbered, False) Then
                        builder.AppendLine($"{player.Character.FullName} is encumbered.")
                    End If
                    Return
                End If
                player.Move(builder)
                ShowCurrentLocation(player, builder)
            End Sub)
    End Sub
End Module
