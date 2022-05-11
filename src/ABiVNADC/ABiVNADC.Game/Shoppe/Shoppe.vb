Public Class Shoppe
    ReadOnly Property Id As Long
    Sub New(shoppeId As Long)
        Id = shoppeId
    End Sub

    Public ReadOnly Property Name As String
        Get
            Return ShoppeData.ReadShoppeName(Id)
        End Get
    End Property

    Public ReadOnly Property InsideLocation As Location
        Get
            Return Location.FromId(ShoppeData.ReadInsideLocation(Id))
        End Get
    End Property

    Public Function CreditBalance(character As Character) As Long
        Return If(ShoppeAccountsData.ReadBalance(Id, character.Id), 0)
    End Function

    Public ReadOnly Property BuyPrices As Dictionary(Of ItemType, Long)
        Get
            Dim prices As Dictionary(Of Long, Long) = ShoppePriceData.ReadBuyPrices(Id)
            Dim result As New Dictionary(Of ItemType, Long)
            For Each price In prices
                result(CType(price.Key, ItemType)) = price.Value
            Next
            Return result
        End Get
    End Property

    Public ReadOnly Property SellPrices As Dictionary(Of ItemType, Long)
        Get
            Dim prices As Dictionary(Of Long, Long) = ShoppePriceData.ReadSellPrices(Id)
            Dim result As New Dictionary(Of ItemType, Long)
            For Each price In prices
                result(CType(price.Key, ItemType)) = price.Value
            Next
            Return result
        End Get
    End Property

    Function CanSell(itemType As ItemType) As Boolean
        Return SellPrices.ContainsKey(itemType)
    End Function

    Function CanBuy(itemType As ItemType) As Boolean
        Return BuyPrices.ContainsKey(itemType)
    End Function

    Public Function SellItem(character As Character, itemType As ItemType) As Long
        If Not character.Inventory.HasItem(itemType) Then
            Return 0
        End If
        If Not CanSell(itemType) Then
            Return 0
        End If
        Dim item = character.Inventory.StackedItems(itemType).First
        Dim credit = SellPrices(itemType)
        item.Destroy()
        ShoppeAccountsData.Write(Id, character.Id, credit + If(ShoppeAccountsData.ReadBalance(Id, character.Id), 0))
        Return credit
    End Function

    Public Sub BuyItem(character As Character, itemType As ItemType)
        If Not CanBuy(itemType) Then
            Return
        End If
        Dim credit = BuyPrices(itemType)
        Dim balance = If(ShoppeAccountsData.ReadBalance(Id, character.Id), 0)
        If balance < credit Then
            Return
        End If
        ShoppeAccountsData.Write(Id, character.Id, balance - credit)
        Dim item = Game.Item.Create(itemType)
        character.Inventory.Add(item)
    End Sub

    Public ReadOnly Property OutsideLocation As Location
        Get
            Return Location.FromId(ShoppeData.ReadOutsideLocation(Id))
        End Get
    End Property

    Public Shared Function FromId(shoppeId As Long?) As Shoppe
        If shoppeId.HasValue Then
            Return New Shoppe(shoppeId.Value)
        End If
        Return Nothing
    End Function
End Class
