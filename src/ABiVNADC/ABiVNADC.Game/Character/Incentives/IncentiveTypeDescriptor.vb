Public Class IncentiveTypeDescriptor
    ReadOnly Property Name As String
    Sub New(name As String)
        Me.Name = name
    End Sub
End Class
Module IncentiveTypeDescriptorExtensions
    Friend ReadOnly IncentiveTypeDescriptors As New Dictionary(Of IncentiveType, IncentiveTypeDescriptor) From
        {
            {
                IncentiveType.StartingFood,
                New IncentiveTypeDescriptor("starting food")
            }
        }
End Module