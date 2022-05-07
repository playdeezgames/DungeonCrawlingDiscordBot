Module StatusProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("Round here, we only respond to a raw `status` commmand!")
            Return
        End If
        ShowStatus(player, builder)
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
                Dim equipment = character.Equipment
                If equipment.Any Then
                    builder.AppendLine("Equipment:")
                    For Each entry In equipment
                        builder.AppendLine($"- {entry.Key.Name} : {entry.Value.Name}")
                    Next
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
End Module
