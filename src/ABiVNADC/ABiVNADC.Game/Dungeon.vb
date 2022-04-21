Public Class Dungeon
    ReadOnly Property Id As Long
    Sub New(dungeonId As Long)
        Id = dungeonId
    End Sub
    Shared Function FromId(dungeonId As Long) As Dungeon
        Return New Dungeon(dungeonId)
    End Function
    ReadOnly Property Name As String
        Get
            Return DungeonData.ReadName(Id)
        End Get
    End Property
    ReadOnly Property StartingLocation As Location
        Get
            Return New Location(DungeonData.ReadLocation(Id).Value)
        End Get
    End Property
End Class
