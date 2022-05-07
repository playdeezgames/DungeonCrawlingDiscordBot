Module LookProcessor
    Const OutputColumns = 56
    Const OutputRows = 28

    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("Round here, we only respond to a raw `look` commmand!")
            Return
        End If
        Dim character = player.Character
        If character Is Nothing Then
            builder.AppendLine("No current character.")
            Return
        End If
        If character.Location Is Nothing Then
            builder.AppendLine($"{character.Name} is not in a dungeon!")
            Return
        End If
        Dim canvas = DrawPOV(player)
        builder.AppendLine($"```
{canvas.Output}```")
    End Sub

    Friend Function DrawPOV(player As Player) As TextCanvas
        Dim direction = player.AheadDirection.Value
        Dim leftDirection = direction.LeftDirection
        Dim rightDirection = direction.RightDirection
        Dim canvas As New TextCanvas(OutputColumns, OutputRows, "@"c)
        Dim character = player.Character
        Dim location = character.Location
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
        Dim enemy = location.Enemy(character)
        If enemy IsNot Nothing Then
            enemy.CharacterType.Sprite.Render(canvas)
        ElseIf location.HasFeatures Then
            location.Features.First.FeatureType.Sprite.Render(canvas)
        ElseIf Not location.Inventory.IsEmpty Then
            canvas.Render(22, 13, Chest, "."c)
        End If
        Return canvas
    End Function
End Module
