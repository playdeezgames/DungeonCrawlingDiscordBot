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
                        Dim equipment = character.Equipment
                        If Not equipment.Any Then
                            builder.AppendLine($"{character.FullName} has nothing equipped!")
                            Return
                        End If
                        builder.AppendLine($"{character.FullName}'s Equipment:")
                        For Each entry In equipment
                            builder.Append($"- {entry.Key.Name}: {entry.Value.Name}")
                            Dim item = entry.Value
                            If item.HasWeaponDurability Then
                                builder.Append($" (ATK:{MaximumRoll(item.AttackDice)}, DUR:{item.WeaponDurability}/{item.MaximumWeaponDurability})")
                            End If
                            If item.HasArmorDurability Then
                                builder.Append($" (DEF:{MaximumRoll(item.DefendDice)}, DUR:{item.ArmorDurability}/{item.MaximumArmorDurability})")
                            End If
                            builder.AppendLine()
                        Next
                    End Sub)
            End Sub)
    End Sub
End Module
