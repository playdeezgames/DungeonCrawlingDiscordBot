Module StatusProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, we only respond to a raw `status` commmand!"
        End If
        Return ShowStatus(player)
    End Function

    Friend Function ShowStatus(player As Player) As String
        Dim builder As New StringBuilder
        builder.AppendLine("Status:")
        Return RequireCharacter(
            player,
            Function(character)
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
                Return builder.ToString
            End Function)
    End Function
End Module
