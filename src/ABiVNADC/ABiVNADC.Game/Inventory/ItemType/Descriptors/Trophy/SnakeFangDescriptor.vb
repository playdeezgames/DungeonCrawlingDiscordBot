Friend Class SnakeFangDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(False, EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "3d1+2d3"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "snake fang"
    End Function
End Class
