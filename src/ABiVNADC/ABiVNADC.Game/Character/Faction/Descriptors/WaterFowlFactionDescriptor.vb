Friend Class WaterFowlFactionDescriptor
    Inherits FactionDescriptor
    Sub New()
        MyBase.New("waterfowl")
    End Sub
    Public Overrides Function IsEnemy(faction As Faction) As Boolean
        Return True
    End Function
    Public Overrides Function AdjustSpawnCountForTheme(theme As DungeonTheme, count As Long) As Long
        Select Case theme
            Case DungeonTheme.Cavern
                Return count
            Case DungeonTheme.Crypt
                Return count
            Case DungeonTheme.Dungeon
                Return count
            Case DungeonTheme.Ruins
                Return count
            Case DungeonTheme.Sewers
                Return count
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
