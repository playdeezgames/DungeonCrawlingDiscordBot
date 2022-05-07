Module LeftProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `left` and nothing else!"
        End If
        If Not player.CanTurn Then
            Return "You cannot do that now!"
        End If
        player.TurnLeft()
        Dim canvas = DrawPOV(player)
        Return $"```
{canvas.Output}```"
    End Function
End Module
