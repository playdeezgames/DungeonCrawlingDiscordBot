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
    ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return PlayerCharacterData.ReadForPlayer(Id).Select(Function(id) Character.FromId(id))
        End Get
    End Property
End Class
