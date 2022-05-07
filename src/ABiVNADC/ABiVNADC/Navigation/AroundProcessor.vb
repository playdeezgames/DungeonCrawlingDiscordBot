﻿Module AroundProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            AroundText,
            builder,
            Sub()
                If Not player.CanTurn Then
                    builder.AppendLine("You cannot do that now!")
                    Return
                End If
                player.TurnAround()
                Dim canvas = DrawPOV(player)
                builder.AppendLine($"```
{canvas.Output}```")
            End Sub)
    End Sub
End Module
