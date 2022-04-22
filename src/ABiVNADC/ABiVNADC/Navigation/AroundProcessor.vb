Module AroundProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `around` and nothing else!"
        End If
        If player.CanTurn Then
            player.TurnAround()
            Return StatusProcessor.ShowStatus(player)
        End If
        Return "You cannot do that now!"
    End Function
End Module
