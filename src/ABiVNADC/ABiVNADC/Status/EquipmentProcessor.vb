Imports SPLORR.Game

Module EquipmentProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            EquipmentText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        builder.AppendLine($"Total ATK: {MaximumRoll(character.AttackDice)}")
                        builder.AppendLine($"Total DEF: {MaximumRoll(character.DefendDice)}")
                        Dim equipment = character.Equipment
                        If Not equipment.Any Then
                            builder.AppendLine($"{character.FullName} has nothing equipped!")
                            Return
                        End If
                        builder.AppendLine($"{character.FullName}'s Equipment:")
                        For Each entry In equipment
                            builder.Append($"- {entry.Key.Name}: {entry.Value.FullName}")
                            Dim item = entry.Value
                            If item.HasAttackDice Then
                                builder.Append($" | ATK:{MaximumRoll(item.AttackDice)}")
                            End If
                            If item.HasDefendDice Then
                                builder.Append($" | DEF:{MaximumRoll(item.DefendDice)}")
                            End If
                            If item.HasDurability(DurabilityType.Weapon) Then
                                builder.Append($" | WPNDUR:{item.Durability(DurabilityType.Weapon)}/{item.MaximumDurability(DurabilityType.Weapon)}")
                            End If
                            If item.HasDurability(DurabilityType.Armor) Then
                                builder.Append($" | ARMDUR:{item.Durability(DurabilityType.Armor)}/{item.MaximumDurability(DurabilityType.Armor)}")
                            End If
                            builder.AppendLine()
                        Next
                    End Sub)
            End Sub)
    End Sub
End Module
