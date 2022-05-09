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
                                RequireFeature(
                                    location,
                                    FeatureType.DungeonEntrance,
                                    builder,
                                    Sub(feature)
                                        Dim dungeon = location.Dungeon
                                        character.Location = dungeon.StartingLocation
                                        builder.AppendLine($"{character.FullName} enters {dungeon.Name}.")
                                        ShowCurrentLocation(player, builder)
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)

    End Sub
End Module
