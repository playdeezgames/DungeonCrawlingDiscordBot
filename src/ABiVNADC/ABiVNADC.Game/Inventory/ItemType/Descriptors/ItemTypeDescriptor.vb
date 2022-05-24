Imports System.Text

Public Class ItemTypeDescriptor
    ReadOnly Property Name As String
    ReadOnly Property CanUse As Boolean
    Overridable Sub OnUse(character As Character, item As Item, builder As StringBuilder)

    End Sub
    Property EquipSlot As EquipSlot
    Property AttackDice As Func(Of Item, String)
    Property DefendDice As Func(Of Item, String)
    Property Durability As Func(Of DurabilityType, Long)
    Property CanBuyGenerator As Dictionary(Of Boolean, Integer)
    Property CanSellGenerator As Dictionary(Of Boolean, Integer)
    Property BuyPriceDice As String
    Property SellPriceDice As String
    Property QuestTargetWeight As Integer
    Property QuestTargetQuantityDice As String
    Property QuestRewardWeight As Integer
    Property QuestRewardQuantityDice As String
    Property InventoryEncumbrance As Long
    Property EquippedEncumbrance As Long
    Property IsTrophy As Boolean
    Property Aliases As IEnumerable(Of String)
    Property PostCreate As Action(Of Item)
    Property Modifier As Func(Of StatisticType, Item, Long)
    Property HealthModifier As Func(Of Item, Long)
    Property EnergyModifier As Func(Of Item, Long)
    Sub New(name As String, canUse As Boolean)
        Me.Name = name
        Me.CanUse = canUse
        Modifier = Function(s, i) 0
        EquipSlot = EquipSlot.None
        AttackDice = Function(x) "0d1"
        DefendDice = Function(x) "0d1"
        Durability = Function(x) 0
        CanBuyGenerator = RNG.MakeBooleanGenerator(1, 0)
        CanSellGenerator = RNG.MakeBooleanGenerator(1, 0)
        BuyPriceDice = "0d1"
        SellPriceDice = "0d1"
        QuestTargetWeight = 0
        QuestTargetQuantityDice = "0d1"
        QuestRewardWeight = 0
        QuestRewardQuantityDice = "0d1"
        InventoryEncumbrance = 0
        EquippedEncumbrance = 0
        IsTrophy = False
        Aliases = New List(Of String)
        PostCreate = Sub()
                     End Sub
        HealthModifier = Function(item) If(item.HasModifier(ModifierType.Health), item.ModifierLevel(ModifierType.Health), 0)
        EnergyModifier = Function(item) If(item.HasModifier(ModifierType.Energy), item.ModifierLevel(ModifierType.Energy), 0)
    End Sub

    Overridable Function SpawnLocations(difficulty As Difficulty, theme As DungeonTheme, locations As IEnumerable(Of Location)) As IEnumerable(Of Location)
        Return New List(Of Location)
    End Function
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
    Friend ReadOnly ItemTypeDescriptors As New Dictionary(Of ItemType, ItemTypeDescriptor) From
        {
            {
                ItemType.Amulet,
                New AmuletDescriptor
            },
            {
                ItemType.Ankh,
                New AnkhDescriptor
            },
            {
                ItemType.Backpack,
                New BackpackDescriptor
            },
            {
                ItemType.Bandage,
                New BandageDescriptor
            },
            {
                ItemType.BatWing,
                New BatWingDescriptor
            },
            {
                ItemType.BugGuts,
                New BugGutsDescriptor
            },
            {
                ItemType.ChainMail,
                New ChainMailDescriptor
            },
            {
                ItemType.Compass,
                New CompassDescriptor
            },
            {
                ItemType.Dagger,
                New DaggerDescriptor
            },
            {
                ItemType.FishFin,
                New FishFinDescriptor
            },
            {
                ItemType.FishScale,
                New FishScaleDescriptor
            },
            {
                ItemType.Food,
                New FoodDescriptor
            },
            {
                ItemType.GoblinEar,
                New GoblinEarDescriptor
            },
            {
                ItemType.Helmet,
                New HelmetDescriptor
            },
            {
                ItemType.HomeScroll,
                New HomeScrollDescriptor
            },
            {
                ItemType.HomeStone,
                New HomeStoneDescriptor
            },
            {
                ItemType.Jools,
                New JoolsDescriptor
            },
            {
                ItemType.LandClaim,
                New LandClaimDescriptor
            },
            {
                ItemType.LongSword,
                New LongSwordDescriptor
            },
            {
                ItemType.Macguffin,
                New MacguffinDescriptor
            },
            {
                ItemType.OrcTooth,
                New OrcToothDescriptor
            },
            {
                ItemType.PlateMail,
                New PlateMailDescriptor
            },
            {
                ItemType.Potion,
                New PotionDescriptor
            },
            {
                ItemType.RatTail,
                New RatTailDescriptor
            },
            {
                ItemType.RottenFood,
                New RottenFoodDescriptor
            },
            {
                ItemType.Shield,
                New ShieldDescriptor
            },
            {
                ItemType.ShortSword,
                New ShortSwordDescriptor
            },
            {
                ItemType.SkullFragment,
                New SkullFragmentDescriptor
            },
            {
                ItemType.SnakeFang,
                New SnakeFangDescriptor
            },
            {
                ItemType.ThankYouNote,
                New ThankYouNoteDescriptor
            },
            {
                ItemType.Trousers,
                New TrousersDescriptor
            },
            {
                ItemType.ZombieTaint,
                New ZombieTaintDescriptor
            }
        }
End Module
