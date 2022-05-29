﻿Friend Class MountainsDescriptor
    Inherits TerrainTypeDescriptor
    Sub New()
        MyBase.New(2, "a mountainous area")
    End Sub
    Public Overrides Function GeneratePerilThreshold() As Long
        Return RNG.RollDice("10d1+1d10")
    End Function

    Public Overrides Function GeneratePeril() As Long
        Return RNG.RollDice("1d8")
    End Function
End Class
