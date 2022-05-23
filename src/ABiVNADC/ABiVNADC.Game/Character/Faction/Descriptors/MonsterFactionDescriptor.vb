Friend Class MonsterFactionDescriptor
    Inherits FactionDescriptor
    Sub New()
        MyBase.New("monster")
    End Sub
    Public Overrides Function IsEnemy(faction As Faction) As Boolean
        Return faction = Faction.Player
    End Function
End Class
