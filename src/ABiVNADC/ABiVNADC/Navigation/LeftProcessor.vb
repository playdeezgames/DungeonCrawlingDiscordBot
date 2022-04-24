﻿Module LeftProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `left` and nothing else!"
        End If
        If player.CanTurn Then
            player.TurnLeft()
            Dim canvas = DrawPOV(player)
            Return $"```
{canvas.Output}```"
        End If
        Return "You cannot do that now!"
    End Function
End Module
