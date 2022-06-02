Imports System.Text

Friend Class LandClaimDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
    End Sub
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        character.ClaimLand(item, builder)
    End Sub

    Public Overrides Function GetName() As String
        Return "land claim"
    End Function
End Class
