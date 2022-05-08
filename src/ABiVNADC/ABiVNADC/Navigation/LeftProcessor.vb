﻿Module LeftProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            LeftText,
            builder,
            Sub()
                If Not player.CanTurn Then
                    builder.AppendLine("You cannot do that now!")
                    Return
                End If
                player.TurnLeft()
                ShowCurrentLocation(player, builder)
            End Sub)
    End Sub
End Module
