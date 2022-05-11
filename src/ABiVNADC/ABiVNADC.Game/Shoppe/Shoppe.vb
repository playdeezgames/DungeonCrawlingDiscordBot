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
