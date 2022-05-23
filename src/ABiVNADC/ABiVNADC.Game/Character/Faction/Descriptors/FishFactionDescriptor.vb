Friend Class FishFactionDescriptor
    Inherits FactionDescriptor
    Sub New()
        MyBase.New("fish")
    End Sub
    Public Overrides Function IsEnemy(faction As Faction) As Boolean
        Return faction = Faction.Player
    End Function
End Class
