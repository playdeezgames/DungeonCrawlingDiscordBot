﻿Module FeaturesProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is just `features`."
        End If
        If Not player.HasCharacter Then
            Return "You have no current character."
        End If
        Dim character = player.Character
        If Not character.HasLocation Then
            Return $"{character.FullName} is not in a dungeon."
        End If
        Dim features = character.Location.Features
        If Not features.Any Then
            Return $"There are no features here."
        End If
        Return $"Features present: {String.Join(", ", features.Select(Function(x) x.Name))}"
    End Function
End Module