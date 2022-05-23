Imports System.Text

Friend Class LandClaimDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("land claim", True)
    End Sub
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        character.ClaimLand(item, builder)
    End Sub
End Class
