Module LookProcessor
    Const OutputColumns = 56
    Const OutputRows = 28

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
            canvas.Render(16, 2, enemy.CharacterType.Image, "."c)
        ElseIf location.HasFeatures Then
            canvas.Render(22, 2, location.Features.First.FeatureType.Image, "."c)
        ElseIf Not location.Inventory.IsEmpty Then
            canvas.Render(22, 13, Chest, "."c)
        End If
        Return canvas
    End Function
End Module
