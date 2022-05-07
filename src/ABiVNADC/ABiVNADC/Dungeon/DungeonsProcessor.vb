Friend Module DungeonsProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("Round here, just `dungeons` is the command. I'm picky.")
            Return
        End If
        builder.AppendLine("Dungeons:")
        Dim dungeons = player.Dungeons
        If dungeons.Any Then
            For Each dungeon In dungeons
                builder.AppendLine($"- {dungeon.Name}")
            Next
        Else
            builder.AppendLine("(none)")
        End If
    End Sub
End Module
