Module MoveProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `move` and nothing else!"
        End If
        If Not player.CanMove Then
            Return "You cannot do that now!"
        End If
        player.Move()
        Dim canvas = DrawPOV(player)
        Return $"```
{canvas.Output}```"
    End Function
End Module
