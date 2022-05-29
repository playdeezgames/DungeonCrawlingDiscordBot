Friend Class PlainsDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(6, "a flat plains")
    End Sub
    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("20d1+1d20")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d4")
    End Function
End Class
