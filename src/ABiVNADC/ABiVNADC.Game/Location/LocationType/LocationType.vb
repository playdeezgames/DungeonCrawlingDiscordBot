Imports System.Runtime.CompilerServices

Public Enum LocationType As Long
    None
    Dungeon
    Overworld
    Shoppe
    LandClaimOffice
    IncentivesOffice
End Enum
Module LocationTypeExtensions
    <Extension>
    Function IsPOV(locationType As LocationType) As Boolean
        Return LocationTypeDescriptors(locationType).IsPOV
    End Function
    <Extension>
    Function CanRest(locationType As LocationType) As Boolean
        Return LocationTypeDescriptors(locationType).CanRest
    End Function
End Module
