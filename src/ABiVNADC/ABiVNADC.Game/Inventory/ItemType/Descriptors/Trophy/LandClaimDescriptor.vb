Friend Class LandClaimDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("land claim", True)
        SpawnCount = Function(difficulty, locationCount) "0d1"
        UseMessage = Function(name) $"{name} claims this plot of land!"
        OnUse = Sub(character, item, builder)
                    character.ClaimLand(item, builder)
                End Sub
    End Sub
End Class
