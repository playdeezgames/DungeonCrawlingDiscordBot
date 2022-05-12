Module UseProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Use what?",
            builder,
            Sub()
                RequireItemType(
                    tokens,
                    builder,
                    Sub(itemType)
                        If itemType = ItemType.None Then
                            builder.AppendLine($"I don't know what a `{itemType.Name}` is.")
                            Return
                        End If
                        If Not itemType.CanUse Then
                            builder.AppendLine($"Cannot use `{itemType.Name}`.")
                            Return
                        End If
                        RequireCharacter(
                            player,
                            builder,
                            Sub(character)
                                Dim itemStacks = character.Inventory.StackedItems
                                If Not itemStacks.ContainsKey(itemType) Then
                                    builder.AppendLine($"{character.Name} doesn't have any `{itemType.Name}`.")
                                    Return
                                End If
                                Dim item = itemStacks(itemType).First
                                player.UseItem(item, builder)
                                character.PerformCounterAttacks(builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
