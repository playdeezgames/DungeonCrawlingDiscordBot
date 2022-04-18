Module StatusProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, we only respond to a raw `status` commmand!"
        Else
            Dim builder As New StringBuilder
            builder.AppendLine("Status:")
            If player.Character Is Nothing Then
                builder.AppendLine("You don't have a character!")
            Else
                builder.AppendLine("You have a character!")
            End If
            Return builder.ToString
        End If
    End Function
End Module
