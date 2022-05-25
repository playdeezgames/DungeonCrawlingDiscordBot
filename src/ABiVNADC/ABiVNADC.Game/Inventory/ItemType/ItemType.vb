Imports System.Runtime.CompilerServices
Imports System.Text

Public Enum ItemType
    None
    Food
    Potion
    Dagger
    ShortSword
    LongSword
    Trousers
    Helmet
    Shield
    ChainMail
    PlateMail
    GoblinEar
    OrcTooth
    SkullFragment
    ZombieTaint
    FishScale
    FishFin
    Jools
    RottenFood
    Compass
    Macguffin
    ThankYouNote
    LandClaim
    Backpack
    Amulet
    Bandage
    Ankh
    HomeStone
    HomeScroll
    BatWing
    BugGuts
    RatTail
    SnakeFang
    Antidote
End Enum
Public Module ItemTypeExtensions
    Public ReadOnly Property AllItemTypes As IEnumerable(Of ItemType)
        Get
            Return ItemTypeDescriptors.Keys
        End Get
    End Property
    <Extension>
    Sub OnUse(itemType As ItemType, character As Character, item As Item, builder As StringBuilder)
        ItemTypeDescriptors(itemType).OnUse(character, item, builder)
    End Sub

    <Extension>
    Function Name(itemType As ItemType) As String
        Return ItemTypeDescriptors(itemType).Name
    End Function
    <Extension>
    Function CanUse(itemType As ItemType) As Boolean
        Return ItemTypeDescriptors(itemType).CanUse
    End Function
    <Extension>
    Function CanEquip(itemType As ItemType) As Boolean
        Return itemType.EquipSlot <> Game.EquipSlot.None
    End Function

    <Extension>
    Function Aliases(itemType As ItemType) As IEnumerable(Of String)
        Return ItemTypeDescriptors(itemType).Aliases
    End Function

    Public Function ParseItemType(itemTypeName As String) As ItemType
        Return AllItemTypes.SingleOrDefault(Function(x) x.Name = itemTypeName OrElse x.Aliases.Contains(itemTypeName))
    End Function

    <Extension>
    Function EquipSlot(itemType As ItemType) As EquipSlot
        Return ItemTypeDescriptors(itemType).EquipSlot
    End Function

    <Extension>
    Function AttackDice(itemType As ItemType, item As Item) As String
        Dim result = ItemTypeDescriptors(itemType).AttackDice(item)
        If item.HasModifier(ModifierType.Attack) Then
            result = $"{result}+{item.Modifiers(ModifierType.Attack)}d2/2"
        End If
        Return result
    End Function

    <Extension>
    Function HasAttackDice(itemType As ItemType, item As Item) As Boolean
        Return itemType.AttackDice(item) <> "0d1" OrElse item.HasModifier(ModifierType.Attack)
    End Function


    <Extension>
    Function DefendDice(itemType As ItemType, item As Item) As String
        Dim result = ItemTypeDescriptors(itemType).DefendDice(item)
        If item.HasModifier(ModifierType.Defend) Then
            result = $"{result}+{item.Modifiers(ModifierType.Defend)}d3/3"
        End If
        Return result
    End Function

    <Extension>
    Function HasDefendDice(itemType As ItemType, item As Item) As Boolean
        Return itemType.DefendDice(item) <> "0d1" OrElse item.HasModifier(ModifierType.Defend)
    End Function

    <Extension>
    Function HasDurability(itemType As ItemType, durabilityType As DurabilityType) As Boolean
        Return itemType.Durability(durabilityType) > 0
    End Function

    <Extension>
    Function Durability(itemType As ItemType, durabilityType As DurabilityType) As Long
        Return ItemTypeDescriptors(itemType).Durability(durabilityType)
    End Function

    <Extension>
    Function CanBuyGenerator(itemType As ItemType) As Dictionary(Of Boolean, Integer)
        Return ItemTypeDescriptors(itemType).CanBuyGenerator
    End Function

    <Extension>
    Function BuyPriceDice(itemType As ItemType) As String
        Return ItemTypeDescriptors(itemType).BuyPriceDice
    End Function

    <Extension>
    Function CanSellGenerator(itemType As ItemType) As Dictionary(Of Boolean, Integer)
        Return ItemTypeDescriptors(itemType).CanSellGenerator
    End Function

    <Extension>
    Function SellPriceDice(itemType As ItemType) As String
        Return ItemTypeDescriptors(itemType).SellPriceDice
    End Function

    <Extension>
    Function QuestTargetQuantityDice(itemType As ItemType) As String
        Return ItemTypeDescriptors(itemType).QuestTargetQuantityDice
    End Function

    <Extension>
    Function QuestRewardQuantityDice(itemType As ItemType) As String
        Return ItemTypeDescriptors(itemType).QuestRewardQuantityDice
    End Function

    <Extension>
    Function InventoryEncumbrance(itemType As ItemType) As Long
        Return ItemTypeDescriptors(itemType).InventoryEncumbrance
    End Function

    <Extension>
    Function EquippedEncumbrance(itemType As ItemType) As Long
        Return ItemTypeDescriptors(itemType).EquippedEncumbrance
    End Function

    <Extension>
    Function IsTrophy(itemType As ItemType) As Boolean
        Return ItemTypeDescriptors(itemType).IsTrophy
    End Function

    <Extension>
    Sub PostCreate(itemType As ItemType, item As Item)
        ItemTypeDescriptors(itemType).PostCreate.Invoke(item)
    End Sub

    <Extension>
    Function Modifier(itemType As ItemType, statisticType As StatisticType, item As Item) As Long
        Return ItemTypeDescriptors(itemType).Modifier(statisticType, item)
    End Function

    <Extension>
    Function SpawnLocations(itemType As ItemType, difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Return ItemTypeDescriptors(itemType).SpawnLocations(difficulty, theme, locations)
    End Function
End Module