Imports System.Runtime.CompilerServices

Public Enum LocationType As Long
    None
    Dungeon
    Overworld
    Shoppe
End Enum
Module LocationTypeExtensions
    <Extension>
    Function IsPOV(locationType As LocationType) As Boolean
        Return locationType = LocationType.Dungeon
    End Function
    <Extension>
    Function CanRest(locationType As LocationType) As Boolean
        Return locationType = LocationType.Dungeon
    End Function
End Module
