Module StatusProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            StatusText,
            builder,
            Sub()
                ShowStatus(player, builder)
            End Sub)
    End Sub

    Friend Sub ShowStatus(player As Player, builder As StringBuilder)
        builder.AppendLine("Status:")
        RequireCharacter(
            player,
            builder,
            Sub(character)
                builder.AppendLine($"Name: {character.Name}")
                builder.AppendLine($"Class: {character.CharacterType.Name}(level {character.Level})")
                builder.AppendLine($"Experience: {character.Experience}/{character.ExperienceGoal}")
                builder.AppendLine($"Health: {character.Health}/{character.MaximumHealth}")
                builder.AppendLine($"Energy: {character.Energy}/{character.MaximumEnergy}")
                builder.AppendLine($"{character.EndowmentName}: {character.Endowment}/{character.MaximumEndowment}")
                ShowEncumbrance(builder, character)
                Dim effects = character.Effects
                If effects.Any Then
                    builder.Append("Effects: ")
                    builder.AppendJoin(", ", effects.Select(Function(x) x.Name))
                    builder.AppendLine()
                End If
                If Not character.HasLocation Then
                    builder.AppendLine($"{character.Name} is not currently in a dungeon.")
                Else
                    If Not player.Character.Location.Inventory.IsEmpty Then
                        builder.AppendLine("There is stuff on the ground.")
                    End If
                End If
            End Sub)
    End Sub

    Private Sub ShowEncumbrance(builder As StringBuilder, character As Character)
        builder.AppendLine($"Encumbrance: {Math.Max(0, character.Encumbrance)}/{character.MaximumEncumbrance}")
    End Sub
End Module
