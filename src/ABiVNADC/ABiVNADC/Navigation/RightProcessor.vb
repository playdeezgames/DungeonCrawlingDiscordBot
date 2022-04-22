Module RightProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "It's just `right` and nothing else!"
        End If
        Dim direction = player.AheadDirection
        If direction.HasValue Then
            player.SetDirection(direction.Value.RightDirection)
            Return StatusProcessor.Run(player, tokens)
        End If
        Return "You cannot do that now!"
    End Function
End Module
