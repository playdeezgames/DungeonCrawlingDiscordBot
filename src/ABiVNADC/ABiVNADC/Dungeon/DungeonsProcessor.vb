Friend Module DungeonsProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, just `dungeons` is the command. I'm picky."
        End If
        Dim builder As New StringBuilder
        builder.AppendLine("Dungeons:")
        Dim dungeons = player.Dungeons
        If dungeons.Any Then
            For Each dungeon In dungeons
                builder.AppendLine($"- {dungeon.Name}")
            Next
        Else
            builder.AppendLine("(none)")
        End If
        Return builder.ToString
    End Function
End Module
