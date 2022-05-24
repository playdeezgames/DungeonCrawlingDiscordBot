Imports System.Runtime.CompilerServices

Public Enum Faction
    None
    Player
    Goblinoids
    Undead
    Fish
    Monster
    Vermin
End Enum
Module FactionExtensions
    <Extension>
    Function Name(faction As Faction) As String
        Return FactionDescriptors(faction).Name
    End Function
    <Extension>
    Function IsEnemy(faction As Faction, otherFaction As Faction) As Boolean
        Return FactionDescriptors(faction).IsEnemy(otherFaction)
    End Function
End Module