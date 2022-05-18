Module EncumbranceProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            EncumbranceText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        builder.AppendLine($"{character.FullName}'s Encumbrance Details:")
                        builder.AppendLine($"Total Encumbrance: {Math.Max(0, character.Encumbrance)}/{character.MaximumEncumbrance}")
                        For Each entry In character.Equipment
                            If entry.Value.EquippedEncumbrance <> 0 Then
                                builder.AppendLine($"Equipped {entry.Value.Name} : {entry.Value.EquippedEncumbrance}")
                            End If
                        Next
                        For Each itemStack In character.Inventory.StackedItems
                            If itemStack.Key.InventoryEncumbrance <> 0 Then
                                builder.AppendLine($"Held {itemStack.Key.Name} : {itemStack.Value.LongCount} @ {itemStack.Key.InventoryEncumbrance} = {itemStack.Value.LongCount * itemStack.Key.InventoryEncumbrance}")
                            End If
                        Next
                    End Sub)
            End Sub)
    End Sub
End Module
