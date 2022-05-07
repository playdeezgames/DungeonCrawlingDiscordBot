Module EnemiesProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            EnemiesText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireInCombat(
                            character,
                            builder,
                            Sub()
                                builder.AppendLine($"{character.FullName} currently faces:")
                                For Each enemy In character.Location.Enemies(character)
                                    builder.AppendLine($"{enemy.FullName} (health:{enemy.Health}/{enemy.MaximumHealth})")
                                Next
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
