﻿Imports System.Text

Friend Class HomeStoneDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("home stone", True)
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanBuyGenerator = MakeBooleanGenerator(25, 1)
        BuyPriceDice = "1000d1+2d1000"
        InventoryEncumbrance = 50
        Aliases = New List(Of String) From {"stone"}
    End Sub
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
End Class
