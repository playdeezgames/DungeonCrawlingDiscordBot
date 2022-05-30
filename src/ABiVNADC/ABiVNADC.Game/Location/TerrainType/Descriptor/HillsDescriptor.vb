Friend Class HillsDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(3, "a hilly area")
    End Sub

    Public Overrides Sub GenerateWanderingMonster(location As Location)
        Character.Create(CharacterType.Orc, 0, location)
    End Sub

    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("12d1+1d12")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d6")
    End Function
End Class
