Imports System.Text

Friend Class LandClaimDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Function UseMessage(name As String) As String
        Return $"{name} claims this plot of land!"
    End Function
    Sub New()
        MyBase.New("land claim", True)
        SpawnCount = Function(difficulty, locationCount) "0d1"
    End Sub
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        character.ClaimLand(item, builder)
    End Sub
End Class
