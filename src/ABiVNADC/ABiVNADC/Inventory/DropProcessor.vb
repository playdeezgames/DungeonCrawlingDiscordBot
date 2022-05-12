Module DropProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Drop what?",
            builder,
            Sub()
                RequireItemTypeQuantity(
                    tokens,
                    builder,
                    Sub(itemType, quantity)
                        RequireCharacter(
                            player,
                            builder,
                            Sub(character)
                                RequireLocation(
                                    character,
                                    builder,
                                    Sub(location)
                                        Dim itemStacks = character.Inventory.StackedItems
                                        If Not itemStacks.ContainsKey(itemType) Then
                                            builder.AppendLine($"{character.Name} doesn't have any `{itemType.Name}`.")
                                            Return
                                        End If
                                        Dim items = itemStacks(itemType).Take(CInt(quantity))
                                        quantity = items.LongCount
                                        For Each item In items
                                            character.Location.Inventory.Add(item)
                                        Next
                                        builder.AppendLine($"{character.Name} drops {quantity} {itemType.Name}")
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)

    End Sub
End Module
