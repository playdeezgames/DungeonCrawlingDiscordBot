Public Module ShoppeAccountsData
    Friend Const TableName = "ShoppeAccounts"
    Friend Const ShoppeIdColumn = ShoppeData.ShoppeIdColumn
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const BalanceColumn = "Balance"
    Friend Sub Initialize()
        ShoppeData.Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ShoppeIdColumn}] INT NOT NULL,
                [{CharacterIdColumn}] INT NOT NULL,
                [{BalanceColumn}] INT NOT NULL,
                UNIQUE([{ShoppeIdColumn}],[{CharacterIdColumn}]),
                FOREIGN KEY([{ShoppeIdColumn}]) REFERENCES [{ShoppeData.TableName}]([{ShoppeData.ShoppeIdColumn}]),
                FOREIGN KEY([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub
    Public Function ReadBalance(shoppeId As Long, characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{BalanceColumn}] FROM [{TableName}] WHERE [{ShoppeIdColumn}]=@{ShoppeIdColumn} AND [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{ShoppeIdColumn}", shoppeId),
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function

    Friend Sub ClearForCharacter(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Sub Write(shoppeId As Long, characterId As Long, balance As Long)
        ReplaceRecord(AddressOf Initialize, TableName, ShoppeIdColumn, shoppeId, CharacterIdColumn, characterId, BalanceColumn, balance)
    End Sub
End Module
