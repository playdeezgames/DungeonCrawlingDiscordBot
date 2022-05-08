﻿Public Module OverworldLocationData
    Friend Const TableName = "OverworldLocations"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const XColumn = "X"
    Friend Const YColumn = "Y"
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INT NOT NULL UNIQUE,
                [{XColumn}] INT NOT NULL,
                [{YColumn}] INT NOT NULL,
                UNIQUE([{XColumn}],[{YColumn}]),
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub

    Public Function ReadY(locationId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, LocationIdColumn, locationId, YColumn)
    End Function

    Public Function ReadX(locationId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, LocationIdColumn, locationId, XColumn)
    End Function

    Public Function ReadForXY(x As Long, y As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{LocationIdColumn}] FROM [{TableName}] WHERE [{XColumn}]=@{XColumn} AND [{YColumn}]=@{YColumn};",
            MakeParameter($"@{XColumn}", x),
            MakeParameter($"@{YColumn}", y))
    End Function

    Public Sub Write(locationId As Long, x As Long, y As Long)
        ReplaceRecord(AddressOf Initialize, TableName, LocationIdColumn, locationId, XColumn, x, YColumn, y)
    End Sub
End Module
