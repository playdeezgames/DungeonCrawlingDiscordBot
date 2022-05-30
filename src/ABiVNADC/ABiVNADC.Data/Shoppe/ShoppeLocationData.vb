Public Module ShoppeLocationData
    Friend Const TableName = "ShoppeLocations"
    Friend Const ShoppeIdColumn = ShoppeData.ShoppeIdColumn
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        ShoppeData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ShoppeIdColumn}] INT NOT NULL,
                [{LocationIdColumn}] INT NOT NULL UNIQUE
            );")
    End Sub

    Public Sub Write(locationId As Long, shoppeId As Long)
        ReplaceRecord(AddressOf Initialize, TableName, (ShoppeIdColumn, shoppeId), (LocationIdColumn, locationId))
    End Sub

    Public Function ReadForLocation(locationId As Long) As Long?
        Return ReadRecordsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, ShoppeIdColumn, (LocationIdColumn, locationId)).SingleOrDefault
    End Function
End Module
