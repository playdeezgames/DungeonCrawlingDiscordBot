Public Class Player
    ReadOnly Property Id As Long
    Sub New(playerId As Long)
        Id = playerId
    End Sub
    ReadOnly Property Character As Character
        Get
            Return Character.FromId(PlayerData.ReadCharacter(Id))
        End Get
    End Property

    Public Function CreateDungeon(dungeonName As String) As Boolean
        Const MazeColumn = 4
        Const MazeRows = 4
        If DungeonData.ReadCountForPlayerAndDungeonName(Id, dungeonName) = 0 Then
            DungeonData.Create(Id, dungeonName)
            Dim maze As New Maze(Of Direction)(MazeColumn, MazeRows, DirectionWalker)
            maze.Generate()
            'TODO: generate a dungeon
            Return True
        End If
        Return False
    End Function

    Public Function CreateCharacter(characterName As String) As Boolean
        If PlayerCharacterData.ReadCountForPlayerAndCharacterName(Id, characterName) = 0 Then
            Dim characterId = CharacterData.Create(characterName)
            PlayerCharacterData.Write(Id, characterId)
            If Character Is Nothing Then
                SwitchCharacter(characterName)
            End If
            Return True
        End If
        Return False
    End Function

    Public Function SwitchCharacter(characterName As String) As Boolean
        Dim characterIds = PlayerCharacterData.ReadForPlayerAndCharacterName(Id, characterName)
        If characterIds.Any Then
            PlayerData.Write(Id, characterIds.First)
            Return True
        End If
        Return False
    End Function

    ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return PlayerCharacterData.ReadForPlayer(Id).Select(Function(id) Character.FromId(id))
        End Get
    End Property
    ReadOnly Property Dungeons As IEnumerable(Of Dungeon)
        Get
            Return DungeonData.ReadForPlayer(Id).Select(Function(id) Dungeon.FromId(id))
        End Get
    End Property
End Class
