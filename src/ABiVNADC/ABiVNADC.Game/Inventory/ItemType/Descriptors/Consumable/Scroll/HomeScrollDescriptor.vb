Imports System.Text

Friend Class HomeScrollDescriptor
    Inherits ItemTypeDescriptor

    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        Dim homeStones = character.OwnedFeatures.Where(Function(x) x.FeatureType = FeatureType.HomeStone)
        If Not homeStones.Any Then
            builder.AppendLine($"{character.FullName} does not own any home stones!")
            Return
        End If
        If character.Location.LocationType <> LocationType.Overworld Then
            builder.AppendLine($"{character.FullName} must be in the overworld in order to use a home scroll!")
            Return
        End If
        Dim destination = RNG.FromEnumerable(homeStones)
        character.Location = destination.Location
        item.Destroy()
        builder.AppendLine($"{character.FullName} uses the home scroll to transport themselves instantly to one of their home stones.")
    End Sub
    Sub New()
        MyBase.New()
        BuyPriceDice = "50d1+2d50"
        InventoryEncumbrance = 0
        Aliases = New List(Of String) From {"hs", "scroll"}
    End Sub
    Public Overrides Function GenerateCanBuy() As Boolean
        Return RNG.FromGenerator(MakeBooleanGenerator(3, 1))
    End Function
    Public Overrides ReadOnly Property CanUse As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "home scroll"
        End Get
    End Property
End Class
