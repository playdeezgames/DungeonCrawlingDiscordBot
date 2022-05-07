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
                                If tokens.Count = 1 And tokens.Single() = AllText Then
                                    builder.AppendLine(HandleTakeAll(player, character, location))
                                    Return
                                End If
                                RequireItemType(
                                    tokens,
                                    builder,
                                    Sub(itemType)
                                        Dim itemStacks = location.Inventory.StackedItems
                                        If Not itemStacks.ContainsKey(itemType) Then
                                            builder.AppendLine($"There ain't any `{itemType.Name}` in sight.")
                                            Return
                                        End If
                                        Dim item = itemStacks(itemType).First
                                        character.Inventory.Add(item)
                                        builder.AppendLine(DoCounterAttacks(character, $"{character.FullName} picks up {itemType.Name}"))
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub

    Private Function HandleTakeAll(player As Player, character As Character, location As Location) As String
        If location.Inventory.IsEmpty Then
            Return "There's nothing to take!"
        End If
        For Each item In location.Inventory.Items
            character.Inventory.Add(item)
        Next
        Return DoCounterAttacks(character, $"{character.FullName} takes everything.
")
    End Function
End Module
