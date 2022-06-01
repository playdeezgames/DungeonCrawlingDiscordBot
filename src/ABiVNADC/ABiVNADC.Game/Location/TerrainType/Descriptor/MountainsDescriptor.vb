Friend Class MountainsDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(2, "a mountainous area")
    End Sub

    Public Overrides Sub GenerateWanderingMonster(location As Location)
        Character.Create(CharacterType.Goose, 0, location) 'TODO: make a new monster
    End Sub

    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("10d1+1d10")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d8")
    End Function
    Public Overrides Function GenerateForage(depletion As Long) As Dictionary(Of ItemType, Long)
        Dim generator As New Dictionary(Of ItemType, Integer) From
            {
                {ItemType.None, 5 + CInt(depletion)},
                {ItemType.Rock, 15}
            }
        Dim result As New Dictionary(Of ItemType, Long)
        Dim generated = RNG.FromGenerator(generator)
        If generated <> ItemType.None Then
            result(generated) = 1
        End If
        Return result
    End Function
End Class
