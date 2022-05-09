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
            Return FeatureType.Name
        End Get
    End Property

    ReadOnly Property FullName As String
        Get
            Select Case FeatureType
                Case FeatureType.DungeonEntrance
                    Return $"the entrance to {Location.Dungeon.Name}"
                Case FeatureType.DungeonExit
                    Return $"the exit from {Location.Dungeon.Name}"
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
