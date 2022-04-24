Module LookProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, we only respond to a raw `look` commmand!"
        End If
        Dim character = player.Character
        If character Is Nothing Then
            Return "No current character."
        End If
        If character.Location Is Nothing Then
            Return $"{character.Name} is not in a dungeon!"
        End If
        Dim canvas = DrawPOV(player)
        Return $"```
{canvas.Output}```"
    End Function
End Module
