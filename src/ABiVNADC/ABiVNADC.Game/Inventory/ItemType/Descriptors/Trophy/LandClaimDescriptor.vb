Imports System.Text

Friend Class LandClaimDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(True, EquipSlot.None)
    End Sub
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        character.ClaimLand(item, builder)
    End Sub

    Public Overrides Function GetName() As String
        Return "land claim"
    End Function
End Class
