Friend Class BatWingDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "2d1+2d2"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "bat wing"
    End Function
End Class
