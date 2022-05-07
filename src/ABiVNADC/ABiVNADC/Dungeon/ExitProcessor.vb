Module ExitProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is just `exit`."
        End If
        If Not player.HasCharacter Then
            Return "You have no current character."
        End If
        Dim character = player.Character
        If Not character.HasLocation Then
            Return $"{character.FullName} is not in a dungeon."
        End If
        If character.InCombat Then
            Return $"{character.FullName} cannot exit while in combat!"
        End If
        Dim location = character.Location
        If Not location.Features.Any(Function(x) x.FeatureType = FeatureType.DungeonExit) Then
            Return "There is no exit here!"
        End If
        character.Location = Nothing
        Return $"{character.FullName} leaves the dungeon."
    End Function
End Module
