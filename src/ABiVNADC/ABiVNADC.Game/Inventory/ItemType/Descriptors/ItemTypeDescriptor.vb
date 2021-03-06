Imports System.Text

Public MustInherit Class ItemTypeDescriptor

    Public MustOverride ReadOnly Property Name As String

    Public Overridable ReadOnly Property CanUse As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property EquipSlot As EquipSlot
        Get
            Return EquipSlot.None
        End Get
    End Property

    Overridable ReadOnly Property Recipes As IReadOnlyList(Of Recipe)
        Get
            Return New List(Of Recipe)
        End Get
    End Property

    Overridable Sub OnUse(character As Character, item As Item, builder As StringBuilder)

    End Sub

    Overridable Function AttackDice(item As Item) As String
        Return "0d1"
    End Function
    Overridable Function DefendDice(item As Item) As String
        Return "0d1"
    End Function

    Overridable Function Durability(durabilityType As DurabilityType) As Long
        Return 0
    End Function
    Overridable Function GenerateCanBuy() As Boolean
        Return False
    End Function
    Property GenerateCanSell As Dictionary(Of Boolean, Integer)
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
    Overridable ReadOnly Property CanThrow As Boolean
        Get
            Return False
        End Get
    End Property

    Sub New()

        Modifier = Function(s, i) 0
        GenerateCanSell = RNG.MakeBooleanGenerator(1, 0)
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

    Overridable Sub OnThrow(character As Character, item As Item, location As Location, builder As StringBuilder)
        'by default, do nothing!
    End Sub
End Class
Module ItemTypeDescriptorUtility
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
                ItemType.Antidote,
                New AntidoteDescriptor
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
                ItemType.EscapeScroll,
                New EscapeScrollDescriptor
            },
            {
                ItemType.FireScroll,
                New FireScrollDescriptor
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
                ItemType.GoldenEgg,
                New GoldenEggDescriptor
            },
            {
                ItemType.GooseEgg,
                New GooseEggDescriptor
            },
            {
                ItemType.GooseFeather,
                New GooseFeatherDescriptor
            },
            {
                ItemType.GoosePoop,
                New GoosePoopDescriptor
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
                ItemType.PlantFiber,
                New PlantFiberDescriptor
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
                ItemType.Rock,
                New RockDescriptor
            },
            {
                ItemType.RottenFood,
                New RottenFoodDescriptor
            },
            {
                ItemType.SharpRock,
                New SharpRockDescriptor
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
                ItemType.SleepScroll,
                New SleepScrollDescriptor
            },
            {
                ItemType.SnakeFang,
                New SnakeFangDescriptor
            },
            {
                ItemType.Stick,
                New StickDescriptor
            },
            {
                ItemType.StoneSpear,
                New StoneSpearDescriptor
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
                ItemType.Twine,
                New TwineDescriptor
            },
            {
                ItemType.ZombieTaint,
                New ZombieTaintDescriptor
            }
        }
End Module
