Module LookProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, we only respond to a raw `look` commmand!"
        Else
            Dim canvas = DrawPOV(player)
            Return $"```
{canvas.Output}```"
        End If
    End Function
End Module
