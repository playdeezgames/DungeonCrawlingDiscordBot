Public Class EndowmentTypeDescriptor
    Property Name As String
End Class
Module EndowmentTypeDescriptorExtensions
    Friend ReadOnly EndowmentTypeDescriptors As New Dictionary(Of EndowmentType, EndowmentTypeDescriptor) From
        {
            {
                EndowmentType.Guile,
                New EndowmentTypeDescriptor With
                {
                    .Name = "Guile"
                }
            },
            {
                EndowmentType.Mana,
                New EndowmentTypeDescriptor With
                {
                    .Name = "Mana"
                }
            },
            {
                EndowmentType.Might,
                New EndowmentTypeDescriptor With
                {
                    .Name = "Might"
                }
            },
            {
                EndowmentType.None,
                New EndowmentTypeDescriptor With
                {
                    .Name = "Potential"
                }
            },
            {
                EndowmentType.Spirit,
                New EndowmentTypeDescriptor With
                {
                    .Name = "Spirit"
                }
            }
        }
End Module
