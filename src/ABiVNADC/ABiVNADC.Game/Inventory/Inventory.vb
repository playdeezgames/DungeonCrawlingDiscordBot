Imports System.Text

Public Class Inventory
    ReadOnly Property Id As Long
    Sub New(inventoryId As Long)
        Id = inventoryId
    End Sub
    Sub Add(item As Item)
        CharacterEquipSlotData.ClearForItem(item.Id)
        InventoryItemData.Write(Id, item.Id)
    End Sub
    ReadOnly Property IsEmpty As Boolean
        Get
            Return InventoryItemData.ReadCountForInventory(Id) = 0
        End Get
    End Property

    ReadOnly Property InventoryEncumbrance As Long
        Get
            Return Items.Sum(Function(x) x.InventoryEncumbrance)
        End Get
    End Property

    ReadOnly Property Items As IEnumerable(Of Item)
        Get
            Return InventoryItemData.ReadForInventory(Id).Select(Function(id) New Item(id))
        End Get
    End Property

    ReadOnly Property StackedItems As Dictionary(Of ItemType, IEnumerable(Of Item))
        Get
            Dim itemStacks = Items.GroupBy(Function(x) x.ItemType)
            Dim result As New Dictionary(Of ItemType, IEnumerable(Of Item))
            For Each itemStack In itemStacks
                result(itemStack.Key) = itemStack
            Next
            Return result
        End Get
    End Property

    ReadOnly Property NamedStackedItems As Dictionary(Of String, IEnumerable(Of Item))
        Get
            Dim itemStacks = Items.GroupBy(Function(x) x.FullName)
            Dim result As New Dictionary(Of String, IEnumerable(Of Item))
            For Each itemStack In itemStacks
                result(itemStack.Key) = itemStack
            Next
            Return result
        End Get
    End Property

    Friend Sub CraftRecipe(recipe As Recipe)
        If HasIngredients(recipe) Then
            For Each input In recipe.Inputs
                For Each item In Items.Where(Function(x) x.ItemType = input.Key).Take(CInt(input.Value))
                    item.Destroy()
                Next
            Next
            For Each output In recipe.Outputs
                Dim count = output.Value
                While count > 0
                    Add(Item.Create(output.Key))
                    count -= 1
                End While
            Next
        End If
    End Sub

    Function HasItem(itemType As ItemType) As Boolean
        Return Items.Any(Function(x) x.ItemType = itemType)
    End Function

    Friend Sub Remove(item As Item)
        InventoryItemData.ClearForItem(item.Id)
    End Sub

    Function FindItemsByName(name As String) As IEnumerable(Of Item)
        Return Items.Where(Function(x) x.FullName = name OrElse x.ItemType.Name = name OrElse x.ItemType.Aliases.Contains(name))
    End Function

    Private Function ItemCount(itemType As ItemType) As Long
        Return Items.Count(Function(x) x.ItemType = itemType)
    End Function

    Friend Function HasIngredients(recipe As Recipe) As Boolean
        Return recipe.Inputs.All(Function(x) ItemCount(x.Key) >= x.Value)
    End Function
End Class
