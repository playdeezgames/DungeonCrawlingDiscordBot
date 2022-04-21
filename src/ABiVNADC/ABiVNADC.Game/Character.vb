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
    Property Location As Location
        Get
            Dim locationId As Long? = CharacterLocationData.Read(Id)
            If locationId.HasValue Then
                Return New Location(locationId.Value)
            End If
            Return Nothing
        End Get
        Set(value As Location)
            If value Is Nothing Then
                CharacterLocationData.Clear(Id)
            Else
                CharacterLocationData.Write(Id, value.Id)
            End If
        End Set
    End Property
End Class
