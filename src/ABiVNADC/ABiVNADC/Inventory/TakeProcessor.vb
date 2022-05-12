Module TakeProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Take what?",
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
                                If tokens.Count = 1 AndAlso tokens.Single() = AllText Then
                                    HandleTakeAll(player, character, location, builder)
                                    Return
                                End If
                                RequireItemTypeQuantity(
                                    tokens,
                                    builder,
                                    Sub(itemType, quantity)
                                        Dim itemStacks = location.Inventory.StackedItems
                                        If Not itemStacks.ContainsKey(itemType) Then
                                            builder.AppendLine($"There ain't any `{itemType.Name}` in sight.")
                                            Return
                                        End If
                                        Dim items = itemStacks(itemType).Take(CInt(quantity))
                                        quantity = items.LongCount
                                        For Each item In items
                                            character.Inventory.Add(item)
                                        Next
                                        builder.AppendLine($"{character.FullName} picks up {quantity} {itemType.Name}")
                                        PerformCounterAttacks(character, builder)
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub

    Private Sub HandleTakeAll(player As Player, character As Character, location As Location, builder As StringBuilder)
        If location.Inventory.IsEmpty Then
            builder.AppendLine("There's nothing to take!")
        End If
        For Each item In location.Inventory.Items
            character.Inventory.Add(item)
        Next
        builder.AppendLine($"{character.FullName} takes everything.")
        PerformCounterAttacks(character, builder)
    End Sub
End Module
