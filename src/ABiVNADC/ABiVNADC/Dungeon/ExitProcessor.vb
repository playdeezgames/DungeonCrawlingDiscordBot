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
                        If character.InCombat Then
                            builder.AppendLine($"{character.FullName} cannot exit while in combat!")
                        End If
                        RequireFeature(
                            location,
                            FeatureType.DungeonExit,
                            builder,
                            Sub(feature)
                                Dim dungeon = location.Dungeon
                                character.Location = dungeon.OverworldLocation
                                builder.AppendLine($"{character.FullName} leaves the dungeon.")
                                ShowCurrentLocation(player, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
