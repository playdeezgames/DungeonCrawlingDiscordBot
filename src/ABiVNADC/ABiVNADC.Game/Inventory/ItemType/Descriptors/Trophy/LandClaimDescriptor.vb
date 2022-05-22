Friend Class LandClaimDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("land claim")
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanUse = True
        UseMessage = Function(name) $"{name} claims this plot of land!"
        OnUse = Sub(character, item, builder)
                    character.ClaimLand(item, builder)
                End Sub
    End Sub
End Class
