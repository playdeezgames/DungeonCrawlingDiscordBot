Module StatusProcessor
    Const OutputColumns = 56
    Const OutputRows = 28
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, we only respond to a raw `status` commmand!"
        Else
            Return ShowStatus(player)
        End If
    End Function

    Friend Function ShowStatus(player As Player) As String
        Dim builder As New StringBuilder
        builder.AppendLine("Status:")
        If Not player.HasCharacter Then
            builder.AppendLine("You don't have a currently selected character!")
        Else
            Dim character = player.Character
            builder.AppendLine($"Currently selected character: {character.Name}")
            builder.AppendLine($"Class: {character.CharacterType.Name}(level {character.Level})")
            builder.AppendLine($"Health: {character.Health}/{character.MaximumHealth}")

            If Not character.HasLocation Then
                builder.AppendLine($"{character.Name} is not currently in a dungeon.")
            Else
                If Not player.Character.Location.Inventory.IsEmpty Then
                    builder.AppendLine("There is stuff on the ground.")
                End If
            End If
        End If
        Return builder.ToString
    End Function

    Friend Function DrawPOV(player As Player) As TextCanvas
        Dim direction = player.AheadDirection.Value
        Dim leftDirection = direction.LeftDirection
        Dim rightDirection = direction.RightDirection
        Dim canvas As New TextCanvas(OutputColumns, OutputRows, "@"c)
        Dim location = player.Character.Location
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
        If Not location.Inventory.IsEmpty Then
            canvas.Render(22, 13, Chest, "."c)
        End If
        Return canvas
    End Function
End Module
