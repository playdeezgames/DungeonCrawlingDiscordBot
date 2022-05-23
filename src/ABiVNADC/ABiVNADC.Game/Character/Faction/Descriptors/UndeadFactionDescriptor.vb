Friend Class UndeadFactionDescriptor
    Inherits FactionDescriptor
    Sub New()
        MyBase.New("undead")
    End Sub
    Public Overrides Function IsEnemy(faction As Faction) As Boolean
        Return faction = Faction.Player
    End Function
End Class
