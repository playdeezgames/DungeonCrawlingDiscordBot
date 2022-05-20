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
End Enum
Public Module ItemTypeExtensions
    Public ReadOnly Property AllItemTypes As IEnumerable(Of ItemType)
        Get
            Return ItemTypeDescriptors.Keys
        End Get
    End Property
    <Extension>
    Sub OnUse(itemType As ItemType, character As Character, item As Item, builder As StringBuilder)
        ItemTypeDescriptors(itemType).OnUse.Invoke(character, item, builder)
    End Sub
    <Extension>
    Function SpawnCount(itemType As ItemType, locationCount As Long, difficulty As Difficulty) As String
        Return ItemTypeDescriptors(itemType).SpawnCount(difficulty, locationCount)
    End Function

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
    Function UseMessage(itemType As ItemType, characterName As String) As String
        Return ItemTypeDescriptors(itemType).UseMessage(characterName)
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
        Return ItemTypeDescriptors(itemType).AttackDice(item)
    End Function

    <Extension>
    Function HasAttackDice(itemType As ItemType, item As Item) As Boolean
        Return itemType.AttackDice(item) <> "0d1"
    End Function


    <Extension>
    Function DefendDice(itemType As ItemType, item As Item) As String
        Return ItemTypeDescriptors(itemType).DefendDice(item)
    End Function

    <Extension>
    Function HasDefendDice(itemType As ItemType, item As Item) As Boolean
        Return itemType.DefendDice(item) <> "0d1"
    End Function

    <Extension>
    Function HasArmorDurability(itemType As ItemType) As Boolean
        Return itemType.ArmorDurability > 0
    End Function

    <Extension>
    Function ArmorDurability(itemType As ItemType) As Long
        Return ItemTypeDescriptors(itemType).ArmorDurability
    End Function

    <Extension>
    Function HasWeaponDurability(itemType As ItemType) As Boolean
        Return itemType.WeaponDurability > 0
    End Function

    <Extension>
    Function WeaponDurability(itemType As ItemType) As Long
        Return ItemTypeDescriptors(itemType).WeaponDurability
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
End Module