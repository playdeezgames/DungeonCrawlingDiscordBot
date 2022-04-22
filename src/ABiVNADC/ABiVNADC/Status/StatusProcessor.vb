Module StatusProcessor
    Const OutputColumns = 56
    Const OutputRows = 28
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, we only respond to a raw `status` commmand!"
        Else
            Dim builder As New StringBuilder
            builder.AppendLine("Status:")
            Dim character = player.Character
            If character Is Nothing Then
                builder.AppendLine("You don't have a currently selected character!")
            Else
                builder.AppendLine($"Currently selected character: {character.Name}")
                Dim location = character.Location
                If location Is Nothing Then
                    builder.AppendLine($"{character.Name} is not currently in a dungeon.")
                Else
                    Dim direction = player.AheadDirection.Value
                    Dim leftDirection = direction.LeftDirection
                    Dim rightDirection = direction.RightDirection
                    Dim canvas As New TextCanvas(OutputColumns, OutputRows, "@"c)
                    Dim routes = location.Routes
                    If routes.ContainsKey(leftDirection) Then
                        canvas.Render(0, 0, LeftDoor)
                    Else
                        canvas.Render(0, 0, LeftWall)
                    End If
                    If routes.ContainsKey(direction) Then
                        canvas.Render(14, 0, AheadDoor)
                    Else
                        canvas.Render(14, 0, AheadWall)
                    End If
                    If routes.ContainsKey(rightDirection) Then
                        canvas.Render(42, 0, RightDoor)
                    Else
                        canvas.Render(42, 0, RightWall)
                    End If
                    builder.AppendLine($"```
{canvas.Output}```")
                End If
            End If
            Return builder.ToString
        End If
    End Function
End Module
