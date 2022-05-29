Public Class LocationTypeDescriptor
    Property IsPOV As Boolean
    Property CanRest As Boolean
    Sub New()
        IsPOV = False
        CanRest = False
    End Sub
End Class
Module LocationTypeDescriptorUtility
    Friend ReadOnly LocationTypeDescriptors As New Dictionary(Of LocationType, LocationTypeDescriptor) From
        {
            {
                LocationType.Dungeon,
                New LocationTypeDescriptor With
                {
                    .IsPOV = True,
                    .CanRest = True
                }
            },
            {
                LocationType.IncentivesOffice,
                New LocationTypeDescriptor
            },
            {
                LocationType.LandClaimOffice,
                New LocationTypeDescriptor
            },
            {
                LocationType.Overworld,
                New LocationTypeDescriptor
            },
            {
                LocationType.Shoppe,
                New LocationTypeDescriptor
            }
        }
End Module