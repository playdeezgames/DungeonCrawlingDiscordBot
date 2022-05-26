Public Class ElementalDamageTypeDescriptor
    ReadOnly Property Name As String
    Sub New(name As String)
        Me.Name = name
    End Sub
End Class
Module ElementalDamageTypeDescriptorExtensions
    Friend ElementalDamageTypeDescriptors As New Dictionary(Of ElementalDamageType, ElementalDamageTypeDescriptor) From
        {
            {
                ElementalDamageType.Fire,
                New ElementalDamageTypeDescriptor("fire")
            }
        }
End Module