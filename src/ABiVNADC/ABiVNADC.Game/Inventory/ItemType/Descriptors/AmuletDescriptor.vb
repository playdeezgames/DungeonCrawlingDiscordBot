Friend Class AmuletDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "amulet"
        SpawnCount = AddressOf VeryRareSpawn
        EquipSlot = EquipSlot.Neck
        CanBuyGenerator = MakeBooleanGenerator(19, 1)
        BuyPriceDice = "200d1+2d200"
        CanSellGenerator = MakeBooleanGenerator(4, 1)
        SellPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        EquippedEncumbrance = 0
        PostCreate = Sub(item)
                         Dim modifierTable As New Dictionary(Of ModifierType, Integer) From
                            {
                            {ModifierType.None, 16},
                            {ModifierType.Defend, 8},
                            {ModifierType.Energy, 4},
                            {ModifierType.Attack, 2},
                            {ModifierType.Health, 1}
                            }
                         Dim modifier = RNG.FromGenerator(modifierTable)
                         If modifier <> ModifierType.None Then
                             item.AddModifier(modifier, 1)
                         End If
                     End Sub
        CanUse = True
        OnUse = Sub(character, item, builder)
                    If Not item.Modifiers.Any Then
                        builder.AppendLine($"{item.FullName} has no power to confer.")
                        Return
                    End If
                    If Not character.Equipment.Any Then
                        builder.AppendLine($"{character.FullName} has no equipment to confer power to.")
                    End If
                    Dim modifier = RNG.FromEnumerable(item.Modifiers.Where(Function(x) x.Value > 0).Select(Function(x) x.Key))
                    Dim target = RNG.FromEnumerable(character.EquipmentItems)
                    builder.AppendLine($"{character.FullName} confers {modifier.Name} to {target.FullName}.")
                    item.AddModifier(modifier, -1)
                    target.AddModifier(modifier, 1)
                End Sub
    End Sub
End Class
