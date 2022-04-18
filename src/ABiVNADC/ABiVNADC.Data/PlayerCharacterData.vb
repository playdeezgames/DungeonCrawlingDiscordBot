Public Module PlayerCharacterData
    Friend Const TableName = "PlayerCharacters"
    Friend Const PlayerIdColumn = PlayerData.PlayerIdColumn
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Sub Initialize()
        PlayerData.Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{PlayerIdColumn}] INT NOT NULL,
                [{CharacterIdColumn}] INT NOT NULL UNIQUE,
                UNIQUE([{PlayerIdColumn}],[{CharacterIdColumn}]),
                FOREIGN KEY ([{PlayerIdColumn}]) REFERENCES [{PlayerData.TableName}]([{PlayerData.PlayerIdColumn}]),
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub
    Public Function ReadForPlayer(playerId As Long) As IEnumerable(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, PlayerIdColumn, playerId)
    End Function
End Module
