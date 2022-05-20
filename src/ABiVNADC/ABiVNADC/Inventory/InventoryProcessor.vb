Module InventoryProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            InventoryText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        Dim inventory = character.Inventory
                        If inventory.IsEmpty Then
                            builder.AppendLine($"{character.FullName} has nothing in their inventory.")
                            Return
                        End If
                        Dim itemStacks = inventory.NamedStackedItems
                        builder.AppendLine($"{character.FullName}'s Inventory: {String.Join(", ", itemStacks.Select(Function(entry) $"{entry.Key}(x{entry.Value.Count})"))}")
                    End Sub)
            End Sub)
    End Sub
End Module
