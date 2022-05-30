Public Module PlayerCharacterData
    Friend Const TableName = "PlayerCharacters"
    Friend Const PlayerIdColumn = PlayerData.PlayerIdColumn
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const DirectionColumn = "Direction"
    Friend Sub Initialize()
        PlayerData.Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{PlayerIdColumn}] INT NOT NULL,
                [{CharacterIdColumn}] INT NOT NULL UNIQUE,
                [{DirectionColumn}] INT NOT NULL,
                UNIQUE([{PlayerIdColumn}],[{CharacterIdColumn}]),
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub

    Public Sub Write(playerId As Long, characterId As Long, direction As Long)
        ReplaceRecord(
            AddressOf Initialize,
            TableName,
            (PlayerIdColumn, playerId),
            (CharacterIdColumn, characterId),
            (DirectionColumn, direction))
    End Sub

    Friend Sub ClearForCharacter(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, (CharacterIdColumn, characterId))
    End Sub

    Public Function ReadForPlayerAndCharacterName(playerId As Long, characterName As String) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader(CharacterIdColumn)),
            $"SELECT 
                pc.[{CharacterIdColumn}]
            FROM 
                [{TableName}] pc 
                JOIN [{CharacterData.TableName}] c ON 
                    pc.[{CharacterIdColumn}]=c.[{CharacterData.CharacterIdColumn}] 
            WHERE 
                pc.[{PlayerIdColumn}]=@{PlayerIdColumn} 
                AND c.[{CharacterData.CharacterNameColumn}]=@{CharacterData.CharacterNameColumn};",
            MakeParameter($"@{PlayerIdColumn}", playerId),
            MakeParameter($"@{CharacterData.CharacterNameColumn}", characterName))
    End Function

    Public Sub WriteDirectionForPlayer(playerId As Long, direction As Long)
        WriteColumnValue(AddressOf Initialize, TableName, (PlayerIdColumn, playerId), (DirectionColumn, direction))
    End Sub

    Public Function ReadForPlayer(playerId As Long) As IEnumerable(Of Long)
        Return ReadRecordsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, CharacterIdColumn, (PlayerIdColumn, playerId))
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

    Public Function ReadDirection(playerId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, DirectionColumn, (PlayerIdColumn, playerId))
    End Function

    Public Function ReadForCharacter(characterId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, PlayerIdColumn, (CharacterIdColumn, characterId))
    End Function
End Module
