﻿Module InventoryProcessor
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
                            builder.AppendLine($"{character.Name} has nothing in their inventory.")
                            Return
                        End If
                        Dim itemStacks = inventory.StackedItems
                        builder.AppendLine($"{character.Name}'s Inventory: {String.Join(", ", itemStacks.Select(Function(entry) $"{entry.Key.Name}(x{entry.Value.Count})"))}")
                    End Sub)
            End Sub)
    End Sub
End Module
