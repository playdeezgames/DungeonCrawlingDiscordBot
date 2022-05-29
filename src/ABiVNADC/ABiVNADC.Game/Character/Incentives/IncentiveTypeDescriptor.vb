Public MustInherit Class IncentiveTypeDescriptor
    ReadOnly Property Name As String
    ReadOnly BasePrice As Long
    Sub New(name As String, basePrice As Long)
        Me.Name = name
        Me.BasePrice = basePrice
    End Sub

    Friend Function IncentivePrice(level As Long) As Long
        If level < 0 Then
            Return 0
        End If
        Return CLng(BasePrice * 2 ^ level)
    End Function

    MustOverride Sub ApplyTo(character As Character, level As Long)
End Class
Module IncentiveTypeDescriptorUtility
    Friend ReadOnly IncentiveTypeDescriptors As New Dictionary(Of IncentiveType, IncentiveTypeDescriptor) From
        {
            {
                IncentiveType.StartingFood,
                New StartingFoodIncentiveDescriptor
            }
        }
End Module