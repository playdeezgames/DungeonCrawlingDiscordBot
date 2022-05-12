Module BribeProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Bribe with what? Empty handed bribes are rude!",
            builder,
            Sub()
                RequireItemType(
                    tokens,
                    builder,
                    Sub(itemType)
                        RequireCharacter(
                            player,
                            builder,
                            Sub(character)
                                RequireLocation(
                                    character,
                                    builder,
                                    Sub(location)
                                        If Not character.Inventory.StackedItems.Keys.Contains(itemType) Then
                                            builder.AppendLine($"{character.FullName} doesn't have any {itemType.Name}.")
                                            Return
                                        End If
                                        Dim enemy As Character = character.Location.Enemies(character).FirstOrDefault(Function(x) x.TakesBribe(itemType))
                                        If enemy Is Nothing Then
                                            builder.AppendLine($"No enemy in this location will take that.")
                                            Return
                                        End If
                                        character.BribeEnemy(enemy, itemType, builder)
                                        character.PerformCounterAttacks(builder)
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
