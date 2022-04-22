Module MoveProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `left` and nothing else!"
        End If
        Dim direction = player.AheadDirection
        If direction.HasValue Then
            player.Move()
            Return StatusProcessor.Run(player, tokens)
        End If
        Return "You cannot do that now!"
    End Function
End Module
