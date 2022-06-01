Public Class Recipe
    ReadOnly Property Inputs As IReadOnlyDictionary(Of ItemType, Long)
    ReadOnly Property Outputs As IReadOnlyDictionary(Of ItemType, Long)
    Sub New(inputs As IReadOnlyDictionary(Of ItemType, Long), outputs As IReadOnlyDictionary(Of ItemType, Long))
        Me.Inputs = inputs
        Me.Outputs = outputs
    End Sub
End Class
