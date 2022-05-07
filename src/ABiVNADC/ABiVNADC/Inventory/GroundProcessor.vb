Module GroundProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            GroundText,
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
                                Dim inventory = location.Inventory
                                If inventory.IsEmpty Then
                                    builder.AppendLine("There is nothing on the ground.")
                                    Return
                                End If
                                Dim itemStacks = inventory.StackedItems
                                builder.AppendLine($"Items on the ground: {String.Join(", ", itemStacks.Select(Function(entry) $"{entry.Key.Name}(x{entry.Value.Count})"))}")
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
