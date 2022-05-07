Module DeleteCharacterProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        'TODO: deprecate me
        If Not tokens.Any Then
            builder.AppendLine($"The syntax is: `delete character (name)`")
            Return
        End If
        Dim characterName = String.Join(" "c, tokens)
        If player.DeleteCharacter(characterName) Then
            builder.AppendLine($"You delete {characterName}.")
            Return
        End If
        builder.AppendLine($"Failed to delete {characterName}!")
    End Sub
End Module
