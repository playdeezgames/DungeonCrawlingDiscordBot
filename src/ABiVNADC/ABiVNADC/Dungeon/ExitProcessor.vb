Module ExitProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("The command is just `exit`.")
            Return
        End If
        RequireCharacter(
            player,
            builder,
            Sub(character)
                RequireLocation(
                    character,
                    builder,
                    Sub(location)
                        If Not location.HasRoute(Direction.Outward) Then
                            builder.AppendLine("There is no exit here.")
                            Return
                        End If
                        If character.InCombat Then
                            builder.AppendLine($"{character.FullName} cannot exit while in combat!")
                            Return
                        End If
                        character.Location = location.Routes(Direction.Outward).ToLocation
                        builder.AppendLine($"{character.FullName} exits.")
                        ShowCurrentLocation(player, builder)
                    End Sub)
            End Sub)
    End Sub
End Module
