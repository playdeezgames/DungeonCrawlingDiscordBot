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
    End Sub
End Class
Module ItemTypeDescriptorExtensions
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
                ItemType.ChainMail,
                New ItemTypeDescriptor With
                {
                    .Name = "chain mail",
                    .SpawnCount = AddressOf RareSpawn,
                    .EquipSlot = EquipSlot.Body,
                    .DefendDice = "1d3/3",
                    .ArmorDurability = 20,
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "75d1+2d75"
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
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "12d1+2d12"
                }
            },
            {
                ItemType.FishFin,
                New ItemTypeDescriptor With
                {
                    .Name = "fish fin",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "40d1+2d40"
                }
            },
            {
                ItemType.FishScale,
                New ItemTypeDescriptor With
                {
                    .Name = "fish scale",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "20d1+2d20"
                }
            },
            {
                ItemType.Food,
                New ItemTypeDescriptor With
                {
                    .Name = "food",
                    .SpawnCount = AddressOf VeryCommonSpawn,
                    .CanUse = True,
                    .UseMessage = Function(x) $"{x} eats food",
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "2d1+2d2"
                }
            },
            {
                ItemType.GoblinEar,
                New ItemTypeDescriptor With
                {
                    .Name = "goblin ear",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "2d1+2d2"
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
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "12d1+2d12"
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
                    .SellPriceDice = "100d1"
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
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "50d1+2d50"
                }
            },
            {
                ItemType.OrcTooth,
                New ItemTypeDescriptor With
                {
                    .Name = "orc tooth",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "5d1+2d5"
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
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "150d1+2d150"
                }
            },
            {
                ItemType.Potion,
                New ItemTypeDescriptor With
                {
                    .Name = "potion",
                    .SpawnCount = AddressOf CommonSpawn,
                    .CanUse = True,
                    .UseMessage = Function(x) $"{x} drinks a potion",
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "25d1+2d25"
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
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "25d1+2d25"
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
                    .CanBuyGenerator = MakeBooleanGenerator(1, 1),
                    .BuyPriceDice = "25d1+2d25"
                }
            },
            {
                ItemType.SkullFragment,
                New ItemTypeDescriptor With
                {
                    .Name = "skull fragment",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "5d1+2d5"
                }
            },
            {
                ItemType.Trousers,
                New ItemTypeDescriptor With
                {
                    .Name = "trousers",
                    .SpawnCount = AddressOf RareSpawn,
                    .EquipSlot = EquipSlot.Legs,
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "50d1+2d50"
                }
            },
            {
                ItemType.ZombieTaint,
                New ItemTypeDescriptor With
                {
                    .Name = "zombie taint",
                    .SpawnCount = Function(difficulty, locationCount) "0d1",
                    .CanSellGenerator = MakeBooleanGenerator(1, 1),
                    .SellPriceDice = "30d1+2d30"
                }
            }
        }
End Module
