Imports System.Text

Friend Class LandClaimDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("land claim", True)
        SpawnCount = Function(difficulty, locationCount) "0d1"
    End Sub
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        character.ClaimLand(item, builder)
    End Sub
End Class
