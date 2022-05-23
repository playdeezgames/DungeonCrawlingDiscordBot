Imports System.Runtime.CompilerServices

Public Enum Faction
    None
    Player
    Goblinoids
    Undead
    Fish
    Monster
End Enum
Module FactionExtensions
    <Extension>
    Function Name(faction As Faction) As String
        Return FactionDescriptors(faction).Name
    End Function
End Module