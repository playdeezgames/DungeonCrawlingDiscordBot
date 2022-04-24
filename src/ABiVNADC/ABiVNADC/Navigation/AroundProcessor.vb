Module AroundProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `around` and nothing else!"
        End If
        If player.CanTurn Then
            player.TurnAround()
            Dim canvas = DrawPOV(player)
            Return $"```
{canvas.Output}```"
        End If
        Return "You cannot do that now!"
    End Function
End Module
