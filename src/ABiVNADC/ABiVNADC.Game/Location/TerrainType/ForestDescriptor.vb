Friend Class ForestDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(6, "a forest")
    End Sub
    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("6d1+1d6")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d12")
    End Function
End Class
