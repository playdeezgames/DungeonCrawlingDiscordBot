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
    Public Sub Write(shoppeId As Long, itemType As Long, buyPrice As Long, sellPrice As Long)
        ReplaceRecord(AddressOf Initialize, TableName, ShoppeIdColumn, shoppeId, ItemTypeColumn, itemType, BuyPriceColumn, buyPrice, SellPriceColumn, sellPrice)
    End Sub
End Module
