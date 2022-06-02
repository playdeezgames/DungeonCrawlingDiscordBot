﻿Friend Class TwineDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
    End Sub
    Public Overrides ReadOnly Property Recipes As IReadOnlyList(Of Recipe)
        Get
            Return New List(Of Recipe) From
            {
                New Recipe(
                    New Dictionary(Of ItemType, Long) From {{ItemType.PlantFiber, 2}},
                    New Dictionary(Of ItemType, Long) From {{ItemType.Twine, 1}})
            }
        End Get
    End Property

    Public Overrides Function GetName() As String
        Return "twine"
    End Function
End Class
