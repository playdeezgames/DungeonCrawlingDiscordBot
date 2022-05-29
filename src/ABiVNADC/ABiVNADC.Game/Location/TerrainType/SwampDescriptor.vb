Friend Class SwampDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(3, "a swamp")
    End Sub
    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("4d1+1d4")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d20")
    End Function
End Class
