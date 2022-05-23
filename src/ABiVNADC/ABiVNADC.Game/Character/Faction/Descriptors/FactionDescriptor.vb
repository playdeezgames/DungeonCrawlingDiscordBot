Friend Class FactionDescriptor
    ReadOnly Property Name As String
    Sub New(name As String)
        Me.Name = name
    End Sub
    Overridable Function IsEnemy(faction As Faction) As Boolean
        Return False
    End Function
End Class
Module FactionDescriptorExtensions
    Friend ReadOnly FactionDescriptors As New Dictionary(Of Faction, FactionDescriptor) From
        {
            {Faction.Fish, New FishFactionDescriptor},
            {Faction.Goblinoids, New GoblinoidFactionDescriptor},
            {Faction.Monster, New MonsterFactionDescriptor},
            {Faction.Player, New PlayerFactionDescriptor},
            {Faction.Undead, New UndeadFactionDescriptor}
        }
End Module
