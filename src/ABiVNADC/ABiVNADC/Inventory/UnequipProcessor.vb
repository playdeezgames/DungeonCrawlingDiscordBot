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
                        RequireItemName(
                            tokens,
                            AddressOf character.FindEquipmentItemsByName,
                            builder,
                            Sub(item)
                                player.Unequip(item, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
