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
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub

    Public Sub Write(playerId As Long, characterId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{PlayerIdColumn}],[{CharacterIdColumn}]) VALUES(@{PlayerIdColumn},@{CharacterIdColumn});",
            MakeParameter($"@{PlayerIdColumn}", playerId),
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Sub

    Public Function ReadForPlayer(playerId As Long) As IEnumerable(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, PlayerIdColumn, playerId)
    End Function

    Public Function ReadCountForPlayerAndCharacterName(playerId As Long, characterName As String) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT 
                COUNT(1) 
            FROM 
                [{TableName}] pc 
                JOIN [{CharacterData.TableName}] c ON 
                    pc.[{CharacterIdColumn}]=c.[{CharacterData.CharacterIdColumn}] 
            WHERE 
                pc.[{PlayerIdColumn}]=@{PlayerIdColumn} 
                AND c.[{CharacterData.CharacterNameColumn}]=@{CharacterData.CharacterNameColumn};",
            MakeParameter($"@{PlayerIdColumn}", playerId),
            MakeParameter($"@{CharacterData.CharacterNameColumn}", characterName)).Value
    End Function
End Module
