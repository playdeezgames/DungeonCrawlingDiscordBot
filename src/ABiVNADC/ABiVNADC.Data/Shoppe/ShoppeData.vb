Public Module ShoppeData
    Friend Const TableName = "Shoppes"
    Friend Const ShoppeIdColumn = "ShoppeId"
    Friend Const ShoppeNameColumn = "ShoppeName"
    Friend Const OutsideLocationIdColumn = "OutsideLocationId"

    Public Function ReadShoppeName(shoppeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, ShoppeNameColumn, (ShoppeIdColumn, shoppeId))
    End Function

    Public Function ReadInsideLocation(shoppeId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, InsideLocationIdColumn, (ShoppeIdColumn, shoppeId))
    End Function

    Public Function ReadOutsideLocation(shoppeId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, OutsideLocationIdColumn, (ShoppeIdColumn, shoppeId))
    End Function

    Friend Const InsideLocationIdColumn = "InsideLocationId"
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ShoppeIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{ShoppeNameColumn}],
                [{OutsideLocationIdColumn}],
                [{InsideLocationIdColumn}],
                FOREIGN KEY ([{OutsideLocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY ([{InsideLocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub
    Public Function Create(shoppeName As String, outsideLocationId As Long, insideLocationId As Long) As Long
        Return CreateRecord(AddressOf Initialize, TableName, (ShoppeNameColumn, shoppeName), (OutsideLocationIdColumn, outsideLocationId), (InsideLocationIdColumn, insideLocationId))
    End Function
End Module
