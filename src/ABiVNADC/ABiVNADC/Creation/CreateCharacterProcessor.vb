Module CreateCharacterProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            $"{CreateText} {CharacterText}",
            builder,
            Sub()
                Dim characterName = String.Join(" "c, tokens)
                If player.CreateCharacter(characterName) Then
                    builder.AppendLine($"You create {characterName}.")
                    Return
                End If
                builder.AppendLine($"Failed to create {characterName}!")
            End Sub)
    End Sub
End Module
