Module GroundProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            GroundText,
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        Dim inventory = location.Inventory
                        If inventory.IsEmpty Then
                            builder.AppendLine("There is nothing on the ground.")
                            Return
                        End If
                        Dim itemStacks = inventory.NamedStackedItems
                        builder.AppendLine($"Items on the ground: {String.Join(", ", itemStacks.Select(Function(entry) $"{entry.Key}(x{entry.Value.Count})"))}")
                    End Sub)
            End Sub)
    End Sub
End Module
