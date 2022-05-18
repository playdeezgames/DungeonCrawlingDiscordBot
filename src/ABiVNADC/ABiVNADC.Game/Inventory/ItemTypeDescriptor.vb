﻿Imports System.Text

Public Class ItemTypeDescriptor
    Property Name As String
    Property SpawnCount As Func(Of Difficulty, Long, String)
    Property CanUse As Boolean
    Property UseMessage As Func(Of String, String)
    Property EquipSlot As EquipSlot
    Property AttackDice As String
    Property DefendDice As String
    Property ArmorDurability As Long
    Property WeaponDurability As Long
    Property CanBuyGenerator As Dictionary(Of Boolean, Integer)
    Property CanSellGenerator As Dictionary(Of Boolean, Integer)
    Property BuyPriceDice As String
    Property SellPriceDice As String
    Property OnUse As Action(Of Character, Item, StringBuilder)
    Property QuestTargetWeight As Integer
    Property QuestTargetQuantityDice As String
    Property QuestRewardWeight As Integer
    Property QuestRewardQuantityDice As String
    Property InventoryEncumbrance As Long
    Property EquippedEncumbrance As Long
    Property IsTrophy As Boolean
    Sub New()
        EquipSlot = EquipSlot.None
        AttackDice = "0d1"
        CanUse = False
        DefendDice = "0d1"
        ArmorDurability = 0
        WeaponDurability = 0
        CanBuyGenerator = RNG.MakeBooleanGenerator(1, 0)
        CanSellGenerator = RNG.MakeBooleanGenerator(1, 0)
        BuyPriceDice = "0d1"
        SellPriceDice = "0d1"
        OnUse = Sub(c, i, b)

                End Sub
        SpawnCount = Function(difficulty, locationCount) "0d1"
        QuestTargetWeight = 0
        QuestTargetQuantityDice = "0d1"
        QuestRewardWeight = 0
        QuestRewardQuantityDice = "0d1"
        InventoryEncumbrance = 0
        EquippedEncumbrance = 0
        IsTrophy = False
    End Sub
End Class
Module ItemTypeDescriptorExtensions
    Friend ReadOnly Property QuestTargetGenerator As Dictionary(Of ItemType, Integer)
        Get
            Dim descriptors = ItemTypeDescriptors
            Dim candidates = descriptors.Where(Function(x) x.Value.QuestTargetWeight > 0)
            Return candidates.
                ToDictionary(Function(x) x.Key, Function(x) x.Value.QuestTargetWeight)
        End Get
    End Property
    Friend ReadOnly Property QuestRewardGenerator As Dictionary(Of ItemType, Integer)
        Get
            Return ItemTypeDescriptors.Where(Function(x) x.Value.QuestRewardWeight > 0).
                ToDictionary(Function(x) x.Key, Function(x) x.Value.QuestRewardWeight)
        End Get
    End Property
    Private Function VeryCommonSpawn(difficulty As Difficulty, locationCount As Long) As String
        Select Case difficulty
            Case Difficulty.Yermom
                Return $"{locationCount * 3 \ 2}d1"
            Case Difficulty.Easy
                Return $"{locationCount}d1"
            Case Difficulty.Normal
                Return $"{locationCount * 3 \ 4}d1"
            Case Difficulty.Difficult
                Return $"{locationCount * 2 \ 3}d1"
            Case Difficulty.Too
                Return $"{locationCount \ 2}d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private Function CommonSpawn(difficulty As Difficulty, locationCount As Long) As String
        Select Case difficulty
            Case Difficulty.Yermom
                Return $"{locationCount * 3 \ 4}d1"
            Case Difficulty.Easy
                Return $"{locationCount * 2 \ 3}d1"
            Case Difficulty.Normal
                Return $"{locationCount \ 2}d1"
            Case Difficulty.Difficult
                Return $"{locationCount \ 3}d1"
            Case Difficulty.Too
                Return $"{locationCount \ 4}d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private Function UncommonSpawn(difficulty As Difficulty, locationCount As Long) As String
        Select Case difficulty
            Case Difficulty.Yermom
                Return $"{locationCount \ 2}d1"
            Case Difficulty.Easy
                Return $"{locationCount \ 3}d1"
            Case Difficulty.Normal
                Return $"{locationCount \ 4}d1"
            Case Difficulty.Difficult
                Return $"{locationCount \ 6}d1"
            Case Difficulty.Too
                Return $"{locationCount \ 8}d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private Function RareSpawn(difficulty As Difficulty, locationCount As Long) As String
        Select Case difficulty
            Case Difficulty.Yermom
                Return $"{locationCount \ 3}d1"
            Case Difficulty.Easy
                Return $"{locationCount \ 4}d1"
            Case Difficulty.Normal
                Return $"{locationCount \ 6}d1"
            Case Difficulty.Difficult
                Return $"{locationCount \ 8}d1"
            Case Difficulty.Too
                Return $"{locationCount \ 12}d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private Function VeryRareSpawn(difficulty As Difficulty, locationCount As Long) As String
        Select Case difficulty
            Case Difficulty.Yermom
                Return $"{locationCount \ 4}d1"
            Case Difficulty.Easy
                Return $"{locationCount \ 6}d1"
            Case Difficulty.Normal
                Return $"{locationCount \ 8}d1"
            Case Difficulty.Difficult
                Return $"{locationCount \ 12}d1"
            Case Difficulty.Too
                Return $"{locationCount \ 16}d1"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Friend ReadOnly ItemTypeDescriptors As New Dictionary(Of ItemType, ItemTypeDescriptor) From
        {
            {
                ItemType.Backpack,
                New ItemTypeDescriptor With
                {
                    .Name = "backpack",
                    .SpawnCount = AddressOf VeryRareSpawn,
                    .EquipSlot = EquipSlot.Back,
                    .ArmorDurability = 1,
                    .CanBuyGenerator = MakeBooleanGenerator(9, 1),
                    .BuyPriceDice = "200d1+2d200",
                    .InventoryEncumbrance = 1,
                    .EquippedEncumbrance = -20
                }
            },
            {
                ItemType.ChainMail,
                New ItemTypeDescriptor With
                {
                    .Name = "chain mail",
                    .SpawnCount = AddressOf RareSpawn,
                    .EquipSlot = EquipSlot.Body,
                    .DefendDice = "1d3/3",
                    .ArmorDurability = 20,
                    .CanBuyGenerator = MakeBooleanGenerator(19, 1),
                    .BuyPriceDice = "75d1+2d75",
                    .InventoryEncumbrance = 20,
                    .EquippedEncumbrance = 15
                }
            },
            {
                ItemType.Compass,
                New ItemTypeDescriptor With
                {
                    .Name = "compass",
                    .CanUse = True,
                    .UseMessage = Function(x) $"{x} looks at their compass",
                    .CanBuyGenerator = MakeBooleanGenerator(49, 1),
                    .BuyPriceDice = "500d1+2d500",
                    .OnUse = Sub(character, item, builder)
                                 builder.AppendLine(ItemType.Compass.UseMessage(character.FullName))
                                 builder.AppendLine($"{character.FullName} is facing {character.Player.AheadDirection.Value.Name}")
                             End Sub,
                    .InventoryEncumbrance = 1
                }
            },
            {
                ItemType.Dagger,
                New ItemTypeDescriptor With
                {
                    .Name = "dagger",
                    .SpawnCount = AddressOf UncommonSpawn,
                    .CanUse = True,
                    .UseMessage = Function(x) $"{x} commits seppuku",
                    .EquipSlot = EquipSlot.Weapon,
                    .AttackDice = "1d2/2",
                    .WeaponDurability = 5,
                    .CanBuyGenerator = MakeBooleanGenerator(4, 1),
                    .BuyPriceDice = "12d1+2d12",
                    .OnUse = Sub(character, item, builder)
                                 character.Destroy()
                                 builder.AppendLine(ItemType.Dagger.UseMessage(character.FullName))
                             End Sub,
                    .InventoryEncumbrance = 1,
                    .EquippedEncumbrance = 0
                }
            },
            {
                ItemType.FishFin,
                New ItemTypeDescriptor With
                {
                    .Name = "fish fin",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "40d1+2d40",
                    .IsTrophy = True
                }
            },
            {
                ItemType.FishScale,
                New ItemTypeDescriptor With
                {
                    .Name = "fish scale",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "20d1+2d20",
                    .IsTrophy = True
                }
            },
            {
                ItemType.Food,
                New ItemTypeDescriptor With
                {
                    .Name = "food",
                    .SpawnCount = AddressOf CommonSpawn,
                    .CanUse = True,
                    .UseMessage = Function(x) $"{x} eats food",
                    .CanBuyGenerator = MakeBooleanGenerator(1, 9),
                    .BuyPriceDice = "2d1+2d2",
                    .OnUse = Sub(character, item, builder)
                                 Const FoodFatigueRecovery As Long = 4
                                 builder.AppendLine(ItemType.Food.UseMessage(character.FullName))
                                 character.AddFatigue(-FoodFatigueRecovery)
                                 builder.Append($"{character.FullName} now has {character.Energy} energy.")
                                 item.Destroy()
                             End Sub,
                    .InventoryEncumbrance = 1
                }
            },
            {
                ItemType.GoblinEar,
                New ItemTypeDescriptor With
                {
                    .Name = "goblin ear",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "2d1+2d2",
                    .IsTrophy = True
                }
            },
            {
                ItemType.Helmet,
                New ItemTypeDescriptor With
                {
                    .Name = "helmet",
                    .SpawnCount = AddressOf UncommonSpawn,
                    .EquipSlot = EquipSlot.Head,
                    .DefendDice = "1d3/3",
                    .ArmorDurability = 5,
                    .CanBuyGenerator = MakeBooleanGenerator(4, 1),
                    .BuyPriceDice = "12d1+2d12",
                    .InventoryEncumbrance = 5,
                    .EquippedEncumbrance = 3
                }
            },
            {
                ItemType.Jools,
                New ItemTypeDescriptor With
                {
                    .Name = "jools",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanBuyGenerator = MakeBooleanGenerator(0, 1),
                    .CanSellGenerator = MakeBooleanGenerator(0, 1),
                    .BuyPriceDice = "100d1",
                    .SellPriceDice = "95d1",
                    .InventoryEncumbrance = 1
                }
            },
            {
                ItemType.LandClaim,
                New ItemTypeDescriptor With
                {
                    .Name = "land claim",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanUse = True,
                    .UseMessage = Function(name) $"{name} claims this plot of land!",
                    .OnUse = Sub(character, item, builder)
                                 character.ClaimLand(item, builder)
                             End Sub
                }
            },
            {
                ItemType.LongSword,
                New ItemTypeDescriptor With
                {
                    .Name = "long sword",
                    .SpawnCount = AddressOf VeryRareSpawn,
                    .EquipSlot = EquipSlot.Weapon,
                    .AttackDice = "1d2/2+1d2/2+1d2/2",
                    .WeaponDurability = 20,
                    .CanBuyGenerator = MakeBooleanGenerator(49, 1),
                    .BuyPriceDice = "50d1+2d50",
                    .InventoryEncumbrance = 10,
                    .EquippedEncumbrance = 5
                }
            },
            {
                ItemType.Macguffin,
                New ItemTypeDescriptor With
                {
                    .Name = "macguffin",
                    .QuestTargetWeight = 1,
                    .QuestTargetQuantityDice = "1d1",
                    .SpawnCount = AddressOf RareSpawn,
                    .IsTrophy = True
                }
            },
            {
                ItemType.OrcTooth,
                New ItemTypeDescriptor With
                {
                    .Name = "orc tooth",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "5d1+2d5",
                    .IsTrophy = True
                }
            },
            {
                ItemType.PlateMail,
                New ItemTypeDescriptor With
                {
                    .Name = "plate mail",
                    .SpawnCount = AddressOf VeryRareSpawn,
                    .EquipSlot = EquipSlot.Body,
                    .DefendDice = "1d3/3+1d3/3",
                    .ArmorDurability = 35,
                    .CanBuyGenerator = MakeBooleanGenerator(49, 1),
                    .BuyPriceDice = "150d1+2d150",
                    .InventoryEncumbrance = 30,
                    .EquippedEncumbrance = 20
                }
            },
            {
                ItemType.Potion,
                New ItemTypeDescriptor With
                {
                    .Name = "potion",
                    .SpawnCount = AddressOf RareSpawn,
                    .CanUse = True,
                    .UseMessage = Function(x) $"{x} drinks a potion",
                    .CanBuyGenerator = MakeBooleanGenerator(1, 4),
                    .BuyPriceDice = "50d1+2d50",
                    .OnUse = Sub(character, item, builder)
                                 Const PotionWoundRecovery As Long = 4
                                 builder.AppendLine(ItemType.Potion.UseMessage(character.FullName))
                                 character.AddWounds(-PotionWoundRecovery)
                                 builder.Append($"{character.FullName} now has {character.Health} health.")
                                 item.Destroy()
                             End Sub,
                    .InventoryEncumbrance = 1
                }
            },
            {
                ItemType.RottenFood,
                New ItemTypeDescriptor With
                {
                    .Name = "rotten food",
                    .SpawnCount = AddressOf VeryCommonSpawn,
                    .CanUse = True,
                    .UseMessage = Function(x) $"{x} eats rotten food",
                    .OnUse = Sub(character, item, builder)
                                 Const FoodFatigueRecovery As Long = 4
                                 builder.AppendLine(ItemType.RottenFood.UseMessage(character.FullName))
                                 character.AddFatigue(-FoodFatigueRecovery)
                                 If RNG.RollDice("1d2/2") > 0 Then
                                     character.ChangeEffectDuration(EffectType.Nausea, RNG.RollDice("2d6"))
                                     builder.AppendLine($"{character.FullName} is a little queasy from the tainted food!")
                                 End If
                                 builder.AppendLine($"{character.FullName} now has {character.Energy} energy.")
                                 item.Destroy()
                             End Sub,
                    .InventoryEncumbrance = 1
                }
            },
            {
                ItemType.Shield,
                New ItemTypeDescriptor With
                {
                    .Name = "shield",
                    .SpawnCount = AddressOf UncommonSpawn,
                    .EquipSlot = EquipSlot.Shield,
                    .DefendDice = "1d3/3",
                    .ArmorDurability = 10,
                    .CanBuyGenerator = MakeBooleanGenerator(9, 1),
                    .BuyPriceDice = "25d1+2d25",
                    .InventoryEncumbrance = 10,
                    .EquippedEncumbrance = 7
                }
            },
            {
                ItemType.ShortSword,
                New ItemTypeDescriptor With
                {
                    .Name = "short sword",
                    .SpawnCount = AddressOf RareSpawn,
                    .EquipSlot = EquipSlot.Weapon,
                    .AttackDice = "1d2/2+1d2/2",
                    .WeaponDurability = 10,
                    .CanBuyGenerator = MakeBooleanGenerator(9, 1),
                    .BuyPriceDice = "25d1+2d25",
                    .InventoryEncumbrance = 6,
                    .EquippedEncumbrance = 4
                }
            },
            {
                ItemType.SkullFragment,
                New ItemTypeDescriptor With
                {
                    .Name = "skull fragment",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "5d1+2d5",
                    .IsTrophy = True
                }
            },
            {
                ItemType.ThankYouNote,
                New ItemTypeDescriptor With
                {
                    .Name = "thank you note",
                    .QuestRewardWeight = 1,
                    .QuestRewardQuantityDice = "1d1",
                    .InventoryEncumbrance = -5
                }
            },
            {
                ItemType.Trousers,
                New ItemTypeDescriptor With
                {
                    .Name = "trousers",
                    .SpawnCount = AddressOf RareSpawn,
                    .EquipSlot = EquipSlot.Legs,
                    .ArmorDurability = 1,
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "50d1+2d50",
                    .InventoryEncumbrance = 1,
                    .EquippedEncumbrance = -4
                }
            },
            {
                ItemType.ZombieTaint,
                New ItemTypeDescriptor With
                {
                    .Name = "zombie taint",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "30d1+2d30",
                    .IsTrophy = True
                }
            }
        }
End Module
