Imports System.Text

Friend Class HomeStoneDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(EquipSlot.None)
        CanBuyGenerator = MakeBooleanGenerator(25, 1)
        BuyPriceDice = "1000d1+2d1000"
        InventoryEncumbrance = 50
        Aliases = New List(Of String) From {"stone"}
    End Sub
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        If Not character.Location.HasOwner OrElse character.Location.Owner.Id <> character.Id Then
            builder.AppendLine($"{character.FullName} cannot use a home stone on a location that {character.FullName} does not own!")
            Return
        End If
        If character.Location.HasFeature(FeatureType.HomeStone) Then
            builder.AppendLine($"There is already a home stone here!")
            Return
        End If
        HomeStone.Create(character.Location, character)
        builder.AppendLine($"{character.FullName} places a home stone.")
    End Sub

    Public Overrides Function GetName() As String
        Return "home stone"
    End Function
End Class
