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
            Return ItemType.HasAttackDice
        End Get
    End Property

    ReadOnly Property AttackDice As String
        Get
            Return ItemType.AttackDice
        End Get
    End Property

    ReadOnly Property HasDefendDice As Boolean
        Get
            Return ItemType.HasDefendDice
        End Get
    End Property

    ReadOnly Property DefendDice As String
        Get
            Return ItemType.DefendDice
        End Get
    End Property

    ReadOnly Property Modifiers As IEnumerable(Of ModifierType)
        Get
            Return ItemModifierData.Read(Id).Select(Function(x) CType(x, ModifierType))
        End Get
    End Property

    ReadOnly Property FullName As String
        Get
            Dim name = ItemType.Name
            For Each modifier In Modifiers
                name = modifier.DecorateName(name)
            Next
            Return name
        End Get
    End Property

    ReadOnly Property HasWeaponDurability As Boolean
        Get
            Return ItemType.HasWeaponDurability
        End Get
    End Property

    Friend Function ReduceWeaponDurability(durability As Long) As Boolean
        If HasWeaponDurability Then
            Dim depletion As Long = If(ItemDepletionData.Read(Id), 0) + durability
            If depletion >= ItemType.WeaponDurability Then
                Destroy()
                Return True
            End If
            ItemDepletionData.Write(Id, depletion)
        End If
        Return False
    End Function

    ReadOnly Property HasArmorDurability As Boolean
        Get
            Return ItemType.HasArmorDurability
        End Get
    End Property

    Friend Function ReduceArmorDurability(durability As Long) As Boolean
        If HasArmorDurability Then
            Dim depletion As Long = If(ItemDepletionData.Read(Id), 0) + durability
            If depletion >= ItemType.ArmorDurability Then
                Destroy()
                Return True
            End If
            ItemDepletionData.Write(Id, depletion)
        End If
        Return False
    End Function

    ReadOnly Property MaximumArmorDurability As Long
        Get
            Return ItemType.ArmorDurability
        End Get
    End Property

    ReadOnly Property ArmorDurability As Long
        Get
            Return Math.Max(0, MaximumArmorDurability - If(ItemDepletionData.Read(Id), 0))
        End Get
    End Property

    ReadOnly Property MaximumWeaponDurability As Long
        Get
            Return ItemType.WeaponDurability
        End Get
    End Property

    ReadOnly Property WeaponDurability As Long
        Get
            Return Math.Max(0, MaximumWeaponDurability - If(ItemDepletionData.Read(Id), 0))
        End Get
    End Property

    Friend Sub AddModifier(modifierType As ModifierType)
        ItemModifierData.Write(Id, modifierType)
    End Sub
End Class
