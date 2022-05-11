Public Module ShoppeData
    Friend Const TableName = "Shoppes"
    Friend Const ShoppeIdColumn = "ShoppeId"
    Friend Const ShoppeNameColumn = "ShoppeName"
    Friend Const OutsideLocationIdColumn = "OutsideLocationId"

    Public Function ReadShoppeName(shoppeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, ShoppeIdColumn, shoppeId, ShoppeNameColumn)
    End Function

    Public Function ReadInsideLocation(shoppeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, ShoppeIdColumn, shoppeId, InsideLocationIdColumn)
    End Function

    Public Function ReadOutsideLocation(shoppeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, ShoppeIdColumn, shoppeId, OutsideLocationIdColumn)
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
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{ShoppeNameColumn}],
                [{OutsideLocationIdColumn}],
                [{InsideLocationIdColumn}]
            ) 
            VALUES
            (
                @{ShoppeNameColumn},
                @{OutsideLocationIdColumn},
                @{InsideLocationIdColumn}
            )",
            MakeParameter($"@{ShoppeNameColumn}", shoppeName),
            MakeParameter($"@{OutsideLocationIdColumn}", outsideLocationId),
            MakeParameter($"@{InsideLocationIdColumn}", insideLocationId))
        Return LastInsertRowId
    End Function
End Module
