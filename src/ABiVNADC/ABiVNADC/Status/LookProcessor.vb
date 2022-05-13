Module LookProcessor
    Const OutputColumns = 56
    Const OutputRows = 28

    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            LookText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireLocation(
                            character,
                            builder,
                            Sub(location)
                                ShowCurrentLocation(player, builder)
                            End Sub)
                    End Sub)
            End Sub)
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
        Else
            If location.HasFeatures Then
                For Each feature In location.Features
                    feature.FeatureType.Sprite.Render(canvas)
                Next
            End If
            If Not location.Inventory.IsEmpty Then
                canvas.Render(22, 13, Chest, "."c)
            End If
        End If
        Return canvas
    End Function
End Module
