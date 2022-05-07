Module MoveProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("It's just `move` and nothing else!")
            Return
        End If
        If Not player.CanMove Then
            builder.AppendLine("You cannot do that now!")
            Return
        End If
        player.Move()
        Dim canvas = DrawPOV(player)
        builder.AppendLine($"```
{canvas.Output}```")
    End Sub
End Module
