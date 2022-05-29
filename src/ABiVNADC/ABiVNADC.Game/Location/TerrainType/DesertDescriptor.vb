Friend Class DesertDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(1, "a sandy desert")
    End Sub
    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("8d1+1d8")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d10")
    End Function
End Class
