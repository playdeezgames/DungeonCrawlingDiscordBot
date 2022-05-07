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
                        RequireItemType(
                            tokens,
                            builder,
                            Sub(itemType)
                                Dim output = character.Equip(itemType)
                                builder.AppendLine(DoCounterAttacks(character, output))
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
