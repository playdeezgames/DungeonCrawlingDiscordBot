Public Class Feature
    ReadOnly Property Id As Long
    Sub New(featureId As Long)
        Id = featureId
    End Sub
    Shared Function Create(location As Location, featureType As FeatureType) As Feature
        Return New Feature(FeatureData.Create(location.Id, featureType))
    End Function

    ReadOnly Property Name As String
        Get
            Return FeatureType.Name(Me)
        End Get
    End Property

    ReadOnly Property FullName As String
        Get
            Select Case FeatureType
                Case FeatureType.DungeonEntrance
                    Return $"the entrance to {Location.Dungeon.Name}"
                Case FeatureType.ShoppeEntrance
                    Return $"the entrance to {Location.Shoppe.Name}"
                Case FeatureType.DungeonExit
                    Return $"the exit from {Location.Dungeon.Name}"
                Case FeatureType.ShoppeExit
                    Return $"the exit from {Location.Shoppe.Name}"
                Case FeatureType.ForSaleSign
                    Dim x = Location.OverworldX.Value
                    Dim y = Location.OverworldY.Value
                    Return $"a sign that reads `For Sale {If(y < 0, $"[N]{-y}", $"[S]{y}")}{If(x < 0, $"[W]{-x}", $"[E]{x}")}`"
                Case FeatureType.Corpse
                    Return $"the corpse of {FeatureTextMetadata.Read(Id, FeatureMetadataType.Name)}"
                Case Else
                    Return Name
            End Select
        End Get
    End Property

    ReadOnly Property Location As Location
        Get
            Return New Location(FeatureData.ReadLocation(Id).Value)
        End Get
    End Property

    ReadOnly Property FeatureType As FeatureType
        Get
            Return CType(FeatureData.ReadFeatureType(Id).Value, FeatureType)
        End Get
    End Property
End Class
