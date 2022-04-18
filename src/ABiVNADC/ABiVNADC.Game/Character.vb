Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    ReadOnly Property Name As String
        Get
            Return CharacterData.ReadName(Id)
        End Get
    End Property
    Shared Function FromId(characterId As Long?) As Character
        If characterId.HasValue Then
            Return New Character(characterId.Value)
        End If
        Return Nothing
    End Function
End Class
