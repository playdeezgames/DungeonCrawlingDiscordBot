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
                        If location.HasFeature(FeatureType.DungeonExit) Then
                            Dim dungeon = location.Dungeon
                            character.Location = dungeon.OverworldLocation
                            builder.AppendLine($"{character.FullName} leaves the dungeon.")
                            ShowCurrentLocation(player, builder)
                        ElseIf location.HasFeature(FeatureType.ShoppeExit) Then
                            Dim shoppe = location.Shoppe
                            character.Location = shoppe.OutsideLocation
                            builder.AppendLine($"{character.FullName} leaves {shoppe.Name}.")
                            ShowCurrentLocation(player, builder)
                        End If
                    End Sub)
            End Sub)
    End Sub
End Module
