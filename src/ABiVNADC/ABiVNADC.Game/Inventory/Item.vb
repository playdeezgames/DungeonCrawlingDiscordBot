Public Class Item
    ReadOnly Property Id As Long
    Sub New(itemId As Long)
        Id = itemId
    End Sub
    Shared Function Create(itemType As ItemType) As Item
        Return New Item(ItemData.Create(itemType))
    End Function

    ReadOnly Property ItemType As ItemType
        Get
            Return CType(ItemData.ReadItemType(Id).Value, ItemType)
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

    ReadOnly Property Name As String
        Get
            Return ItemType.Name
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
End Class
