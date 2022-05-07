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
                    Function(location)
                        If character.InCombat Then
                            Return $"{character.FullName} cannot exit while in combat!"
                        End If
                        If Not location.Features.Any(Function(x) x.FeatureType = FeatureType.DungeonExit) Then
                            Return "There is no exit here!"
                        End If
                        character.Location = Nothing
                        Return $"{character.FullName} leaves the dungeon."
                    End Function)
            End Sub)
    End Sub
End Module
