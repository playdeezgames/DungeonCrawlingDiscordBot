Friend Class FishFinDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(False, EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "40d1+2d40"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "fish fin"
    End Function
End Class
