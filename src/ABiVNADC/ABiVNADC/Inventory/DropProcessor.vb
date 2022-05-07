Module DropProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Drop what?",
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
                                        Dim itemStacks = character.Inventory.StackedItems
                                        If Not itemStacks.ContainsKey(itemType) Then
                                            builder.AppendLine($"{character.Name} doesn't have any `{itemType.Name}`.")
                                            Return
                                        End If
                                        Dim item = itemStacks(itemType).First
                                        character.Location.Inventory.Add(item)
                                        builder.AppendLine($"{character.Name} drops {itemType.Name}")
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)

    End Sub
End Module
