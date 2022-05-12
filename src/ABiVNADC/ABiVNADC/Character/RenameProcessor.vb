Module RenameProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireCharacter(
            player,
            builder,
            Sub(character)
                Dim oldName = character.FullName
                character.Rename(StitchTokens(tokens))
                builder.AppendLine($"{oldName} is now known as {character.FullName}.")
            End Sub)
    End Sub
End Module
