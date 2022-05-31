Friend Class SwampDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(3, "a swamp")
    End Sub

    Public Overrides Sub GenerateWanderingMonster(location As Location)
        Character.Create(CharacterType.Snake, 0, location)
    End Sub

    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("4d1+1d4")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d20")
    End Function

    Public Overrides Function GenerateForage(depletion As Long) As Dictionary(Of ItemType, Long)
        Dim generator As New Dictionary(Of ItemType, Integer) From
            {
                {ItemType.None, 10 + CInt(depletion)},
                {ItemType.PlantFiber, 20}
            }
        Dim result As New Dictionary(Of ItemType, Long)
        Dim generated = RNG.FromGenerator(generator)
        If generated <> ItemType.None Then
            result(generated) = 1
        End If
        Return result
    End Function
End Class
