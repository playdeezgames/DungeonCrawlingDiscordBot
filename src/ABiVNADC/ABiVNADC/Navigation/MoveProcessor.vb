Module MoveProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `move` and nothing else!"
        End If
        If player.CanMove Then
            player.Move()
            Return StatusProcessor.ShowStatus(player)
        End If
        Return "You cannot do that now!"
    End Function
End Module
