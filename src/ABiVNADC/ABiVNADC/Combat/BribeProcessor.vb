Module BribeProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            builder.AppendLine("Bribe with what? Empty handed bribes are rude!")
            Return
        End If
        Dim itemTypeName = StitchTokens(tokens)
        Dim itemType = ParseItemType(itemTypeName)
        If itemType = ItemType.None Then
            builder.AppendLine($"I don't know what a `{itemTypeName}` is.")
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
                        If Not character.Inventory.StackedItems.Keys.Contains(itemType) Then
                            builder.AppendLine($"{character.FullName} doesn't have any {itemType.Name}.")
                            Return
                        End If
                        Dim enemy As Character = character.Location.Enemies(character).FirstOrDefault(Function(x) x.TakesBribe(itemType))
                        If enemy Is Nothing Then
                            builder.AppendLine($"No enemy in this location will take that.")
                            Return
                        End If
                        builder.AppendLine(DoCounterAttacks(character, character.BribeEnemy(enemy, itemType)))
                    End Sub)
            End Sub)
    End Sub
End Module
