Friend Class GoblinoidFactionDescriptor
    Inherits FactionDescriptor
    Sub New()
        MyBase.New("goblinoid")
    End Sub
    Public Overrides Function IsEnemy(faction As Faction) As Boolean
        Return faction = Faction.Player
    End Function
    Public Overrides Function AdjustSpawnCountForTheme(theme As DungeonTheme, count As Long) As Long
        Select Case theme
            Case DungeonTheme.Cavern
                Return count \ 2
            Case DungeonTheme.Crypt
                Return count \ 8
            Case DungeonTheme.Dungeon
                Return count \ 4
            Case DungeonTheme.Ruins
                Return count
            Case DungeonTheme.Sewers
                Return count \ 8
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
