Friend Class LandClaimDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Function UseMessage(name As String) As String
        Return $"{name} claims this plot of land!"
    End Function
    Sub New()
        MyBase.New("land claim", True)
        SpawnCount = Function(difficulty, locationCount) "0d1"
        OnUse = Sub(character, item, builder)
                    character.ClaimLand(item, builder)
                End Sub
    End Sub
End Class
