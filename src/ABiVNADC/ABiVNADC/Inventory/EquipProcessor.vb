Module EquipProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            $"{EquipText} (item)",
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireItemName(
                            tokens,
                            AddressOf character.Inventory.FindItemsByName,
                            builder,
                            Sub(item)
                                player.Equip(item, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
