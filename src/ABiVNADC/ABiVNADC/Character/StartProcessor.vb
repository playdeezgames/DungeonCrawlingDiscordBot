Module StartProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        builder.AppendLine("Not ready yet! Soon!")
        Return
        RequireNoTokens(
            tokens,
            StartText,
            builder,
            Sub()
                RequireNoCharacter(
                    player,
                    builder,
                    Sub()
                        player.CreateCharacter()
                        ShowCurrentLocation(player, builder)
                    End Sub)
            End Sub)
    End Sub
End Module
