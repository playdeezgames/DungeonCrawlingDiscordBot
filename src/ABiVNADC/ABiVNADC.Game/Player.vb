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

    Public Function CreateCharacter(characterName As String) As Boolean
        Dim characterId = CharacterData.Create(characterName)
        PlayerCharacterData.Write(Id, characterId)
    End Function

    ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return PlayerCharacterData.ReadForPlayer(Id).Select(Function(id) Character.FromId(id))
        End Get
    End Property
End Class
