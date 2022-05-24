Friend Class VerminFactionDescriptor
    Inherits FactionDescriptor
    Sub New()
        MyBase.New("vermin")
    End Sub
    Public Overrides Function IsEnemy(faction As Faction) As Boolean
        Return faction = Faction.Player
    End Function
End Class
