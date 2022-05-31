Friend Class ForestDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(6, "a forest")
    End Sub

    Public Overrides Sub GenerateWanderingMonster(location As Location)
        Character.Create(CharacterType.Goblin, 0, location)
    End Sub

    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("6d1+1d6")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d12")
    End Function

    Public Overrides Function GenerateForage(depletion As Long) As Dictionary(Of ItemType, Long)
        Dim generator As New Dictionary(Of ItemType, Integer) From
            {
                {ItemType.None, 10 + CInt(depletion)},
                {ItemType.PlantFiber, 10}
            }
        Dim result As New Dictionary(Of ItemType, Long)
        Dim generated = RNG.FromGenerator(generator)
        If generated <> ItemType.None Then
            result(generated) = 1
        End If
        Return result
    End Function
End Class
