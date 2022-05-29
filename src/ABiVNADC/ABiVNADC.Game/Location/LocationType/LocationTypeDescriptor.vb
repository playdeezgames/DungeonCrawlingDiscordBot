Public MustInherit Class LocationTypeDescriptor
    ReadOnly Property IsPOV As Boolean
    ReadOnly Property CanRest As Boolean
    Sub New(isPov As Boolean, canRest As Boolean)
        Me.IsPOV = isPov
        Me.CanRest = canRest
    End Sub

    MustOverride Sub HandleEnteredBy(character As Character, location As Location)
End Class
Module LocationTypeDescriptorUtility
    Friend ReadOnly LocationTypeDescriptors As New Dictionary(Of LocationType, LocationTypeDescriptor) From
        {
            {
                LocationType.Dungeon,
                New DungeonLocationDescriptor
            },
            {
                LocationType.IncentivesOffice,
                New NormalLocationDescriptor
            },
            {
                LocationType.LandClaimOffice,
                New NormalLocationDescriptor
            },
            {
                LocationType.Overworld,
                New OverworldLocationDescriptor
            },
            {
                LocationType.Shoppe,
                New NormalLocationDescriptor
            }
        }
End Module