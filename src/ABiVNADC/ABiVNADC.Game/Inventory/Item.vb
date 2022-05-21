Public Class Item
    ReadOnly Property Id As Long
    Sub New(itemId As Long)
        Id = itemId
    End Sub
    Shared Function Create(itemType As ItemType) As Item
        Dim item = New Item(ItemData.Create(itemType))
        itemType.PostCreate(item)
        Return item
    End Function

    Function StatisticModifier(statisticType As StatisticType) As Long
        Return ItemType.Modifier(statisticType, Me)
    End Function

    ReadOnly Property CanUse As Boolean
        Get
            Return ItemType.CanUse
        End Get
    End Property

    ReadOnly Property ItemType As ItemType
        Get
            Return CType(ItemData.ReadItemType(Id).Value, ItemType)
        End Get
    End Property

    ReadOnly Property IsTrophy As Boolean
        Get
            Return ItemType.IsTrophy
        End Get
    End Property

    ReadOnly Property EquippedEncumbrance As Long
        Get
            Return ItemType.EquippedEncumbrance
        End Get
    End Property

    ReadOnly Property InventoryEncumbrance As Long
        Get
            Return ItemType.InventoryEncumbrance
        End Get
    End Property

    Friend Sub Destroy()
        ItemData.Clear(Id)
    End Sub

    Friend Shared Function FromId(itemId As Long?) As Item
        Return If(itemId.HasValue, New Item(itemId.Value), Nothing)
    End Function

    ReadOnly Property HasAttackDice As Boolean
        Get
            Return ItemType.HasAttackDice(Me)
        End Get
    End Property

    ReadOnly Property AttackDice As String
        Get
            Return ItemType.AttackDice(Me)
        End Get
    End Property

    ReadOnly Property HasDefendDice As Boolean
        Get
            Return ItemType.HasDefendDice(Me) OrElse HasModifier(ModifierType.Defend)
        End Get
    End Property

    ReadOnly Property DefendDice As String
        Get
            Return ItemType.DefendDice(Me)
        End Get
    End Property

    ReadOnly Property Modifiers As Dictionary(Of ModifierType, Long)
        Get
            Return ItemModifierData.Read(Id).
                ToDictionary(
                Function(entry) CType(entry.Key, ModifierType),
                Function(entry) entry.Value)
        End Get
    End Property

    ReadOnly Property FullName As String
        Get
            Dim name = ItemType.Name
            For Each entry In Modifiers.Where(Function(e) e.Value > 0)
                name = entry.Key.DecorateName(name)
            Next
            Return name
        End Get
    End Property

    Function HasDurability(durabilityType As DurabilityType) As Boolean
        Return ItemType.HasDurability(durabilityType)
    End Function

    Friend Function ReduceDurability(durabiltyType As DurabilityType, durability As Long) As Boolean
        If HasDurability(durabiltyType) Then
            Dim depletion As Long = If(ItemDepletionData.Read(Id, durabiltyType), 0) + durability
            If depletion >= ItemType.Durability(durabiltyType) Then
                Destroy()
                Return True
            End If
            ItemDepletionData.Write(Id, durabiltyType, depletion)
        End If
        Return False
    End Function

    Function MaximumDurability(durabilityType As DurabilityType) As Long
        Return ItemType.Durability(durabilityType)
    End Function

    Function Durability(durabilityType As DurabilityType) As Long
        Return Math.Max(0, MaximumDurability(durabilityType) - If(ItemDepletionData.Read(Id, durabilityType), 0))
    End Function

    Friend Sub AddModifier(modifierType As ModifierType, delta As Long)
        ItemModifierData.Write(Id, modifierType, If(ItemModifierData.ReadLevel(Id, modifierType), 0) + delta)
    End Sub

    Friend Function HasModifier(modifierType As ModifierType) As Boolean
        Return Modifiers.ContainsKey(modifierType) AndAlso Modifiers(modifierType) <> 0
    End Function

    Friend Function ModifierLevel(modifierType As ModifierType) As Long
        Return If(ItemModifierData.ReadLevel(Id, modifierType), 0)
    End Function
End Class
