Public Class Corpse
    ReadOnly Property Id As Long
    Sub New(corpseId As Long)
        Id = corpseId
    End Sub
    ReadOnly Property CorpseName As String
        Get
            Return CorpseData.ReadName(Id)
        End Get
    End Property
End Class
