Module RightProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `right` and nothing else!"
        End If
        If Not player.CanTurn Then
            Return "You cannot do that now!"
        End If
        player.TurnRight()
        Dim canvas = DrawPOV(player)
        Return $"```
{canvas.Output}```"
    End Function
End Module
