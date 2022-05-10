Module EnterProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            EnterText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireLocation(
                            character,
                            builder,
                            Sub(location)
                                If location.HasFeature(FeatureType.DungeonEntrance) Then
                                    Dim dungeon = location.Dungeon
                                    character.Location = dungeon.StartingLocation
                                    builder.AppendLine($"{character.FullName} enters {dungeon.Name}.")
                                    ShowCurrentLocation(player, builder)
                                ElseIf location.HasFeature(FeatureType.ShoppeEntrance) Then
                                    Dim shoppe = location.Shoppe
                                    character.Location = shoppe.InsideLocation
                                    builder.AppendLine($"{character.FullName} enters {shoppe.Name}.")
                                    ShowCurrentLocation(player, builder)
                                End If
                            End Sub)
                    End Sub)
            End Sub)

    End Sub
End Module
