Public Class AnkhDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New(EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "100d1+2d100"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "ankh"
    End Function
End Class
