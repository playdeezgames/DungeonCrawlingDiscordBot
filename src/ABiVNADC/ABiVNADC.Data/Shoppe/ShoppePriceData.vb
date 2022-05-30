Public Module ShoppePriceData
    Friend Const TableName = "ShoppePrices"
    Friend Const ShoppeIdColumn = ShoppeData.ShoppeIdColumn
    Friend Const ItemTypeColumn = "ItemType"
    Friend Const BuyPriceColumn = "BuyPrice"
    Friend Const SellPriceColumn = "SellPrice"
    Friend Sub Initialize()
        ShoppeData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ShoppeIdColumn}] INT NOT NULL,
                [{ItemTypeColumn}] INT NOT NULL,
                [{BuyPriceColumn}] INT NOT NULL,
                [{SellPriceColumn}] INT NOT NULL,
                UNIQUE([{ShoppeIdColumn}],[{ItemTypeColumn}]),
                FOREIGN KEY ([{ShoppeIdColumn}]) REFERENCES [{ShoppeData.TableName}]([{ShoppeData.ShoppeIdColumn}])
            );")
    End Sub

    Public Function ReadBuyPrices(shoppeId As Long) As Dictionary(Of Long, Long)
        Initialize()
        Dim result As New Dictionary(Of Long, Long)
        For Each entry In ExecuteReader(
            Function(reader) New Tuple(Of Long, Long)(CLng(reader(ItemTypeColumn)), CLng(reader(BuyPriceColumn))),
            $"SELECT 
                [{ItemTypeColumn}], 
                [{BuyPriceColumn}] 
            FROM 
                [{TableName}] 
            WHERE 
                [{ShoppeIdColumn}]=@{ShoppeIdColumn} AND 
                [{BuyPriceColumn}]>0;",
            MakeParameter($"@{ShoppeIdColumn}", shoppeId))
            result(entry.Item1) = entry.Item2
        Next
        Return result
    End Function

    Public Function ReadSellPrices(shoppeId As Long) As Dictionary(Of Long, Long)
        Initialize()
        Dim result As New Dictionary(Of Long, Long)
        For Each entry In ExecuteReader(
            Function(reader) New Tuple(Of Long, Long)(CLng(reader(ItemTypeColumn)), CLng(reader(SellPriceColumn))),
            $"SELECT 
                [{ItemTypeColumn}], 
                [{SellPriceColumn}] 
            FROM 
                [{TableName}] 
            WHERE 
                [{ShoppeIdColumn}]=@{ShoppeIdColumn} AND 
                [{SellPriceColumn}]>0;",
            MakeParameter($"@{ShoppeIdColumn}", shoppeId))
            result(entry.Item1) = entry.Item2
        Next
        Return result
    End Function

    Public Sub Write(shoppeId As Long, itemType As Long, buyPrice As Long, sellPrice As Long)
        ReplaceRecord(AddressOf Initialize, TableName, (ShoppeIdColumn, shoppeId), (ItemTypeColumn, itemType), (BuyPriceColumn, buyPrice), (SellPriceColumn, sellPrice))
    End Sub
End Module
