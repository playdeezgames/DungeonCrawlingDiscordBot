Friend Class FishFactionDescriptor
    Inherits FactionDescriptor
    Sub New()
        MyBase.New("fish")
    End Sub
    Public Overrides Function IsEnemy(faction As Faction) As Boolean
        Return faction = Faction.Player
    End Function
    Public Overrides Function AdjustSpawnCountForTheme(theme As DungeonTheme, count As Long) As Long
        Select Case theme
            Case DungeonTheme.Cavern
                Return count \ 2
            Case DungeonTheme.Crypt
                Return count \ 4
            Case DungeonTheme.Dungeon
                Return count \ 4
            Case DungeonTheme.Ruins
                Return count \ 4
            Case DungeonTheme.Sewers
                Return count
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
