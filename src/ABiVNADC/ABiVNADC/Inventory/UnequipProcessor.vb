Module UnequipProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Unequip what?",
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireItemType(
                            tokens,
                            builder,
                            Sub(itemType)
                                Dim equipSlots = character.Equipment.Where(Function(x) x.Value.ItemType = itemType)
                                If Not equipSlots.Any Then
                                    builder.AppendLine($"You don't have any `{itemType.Name}` equipped.")
                                End If
                                character.Unequip(equipSlots.First.Value, builder)
                                PerformCounterAttacks(character, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
