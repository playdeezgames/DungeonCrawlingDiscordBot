Public Module LocationData
    Friend Const TableName = "Locations"
    Friend Const LocationIdColumn = "LocationId"
    Friend Const LocationTypeColumn = "LocationType"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{LocationTypeColumn}] INT NOT NULL
            );")
    End Sub
    Public Function Create(locationType As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]([{LocationTypeColumn}]) VALUES(@{LocationTypeColumn});",
            MakeParameter($"@{LocationTypeColumn}", locationType))
        Return LastInsertRowId
    End Function
End Module
