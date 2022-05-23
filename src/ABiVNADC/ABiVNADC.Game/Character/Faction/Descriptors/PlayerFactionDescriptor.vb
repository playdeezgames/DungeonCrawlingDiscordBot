Friend Class PlayerFactionDescriptor
    Inherits FactionDescriptor
    Sub New()
        MyBase.New("player")
    End Sub
    Public Overrides Function IsEnemy(faction As Faction) As Boolean
        Return faction <> Faction.Player
    End Function
End Class
