Imports System.Runtime.CompilerServices

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
End Enum
Public Module ItemTypeExtensions
    Public ReadOnly AllItemTypes As New List(Of ItemType) From
        {
            ItemType.Food,
            ItemType.Potion,
            ItemType.Dagger,
            ItemType.ShortSword,
            ItemType.LongSword,
            ItemType.Shield,
            ItemType.ChainMail,
            ItemType.PlateMail,
            ItemType.Helmet,
            ItemType.Trousers,
            ItemType.GoblinEar,
            ItemType.OrcTooth,
            ItemType.SkullFragment,
            ItemType.ZombieTaint,
            ItemType.FishFin,
            ItemType.FishScale,
            ItemType.Jools
        }

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
    Public Function ParseItemType(itemTypeName As String) As ItemType
        Return AllItemTypes.SingleOrDefault(Function(x) x.Name = itemTypeName)
    End Function

    <Extension>
    Function EquipSlot(itemType As ItemType) As EquipSlot
        Return ItemTypeDescriptors(itemType).EquipSlot
    End Function

    <Extension>
    Function AttackDice(itemType As ItemType) As String
        Return ItemTypeDescriptors(itemType).AttackDice
    End Function

    <Extension>
    Function HasAttackDice(itemType As ItemType) As Boolean
        Return itemType.AttackDice <> "0d1"
    End Function


    <Extension>
    Function DefendDice(itemType As ItemType) As String
        Return ItemTypeDescriptors(itemType).DefendDice
    End Function

    <Extension>
    Function HasDefendDice(itemType As ItemType) As Boolean
        Return itemType.DefendDice <> "0d1"
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
        Return ItemTypeDescriptors(itemType).ArmorDurability
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
End Module