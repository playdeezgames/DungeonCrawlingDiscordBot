Public Class Feature
    ReadOnly Property Id As Long
    Sub New(featureId As Long)
        Id = featureId
    End Sub
    Shared Function Create(location As Location, featureType As FeatureType) As Feature
        Return New Feature(FeatureData.Create(location.Id, featureType))
    End Function

    ReadOnly Property Corpses As IEnumerable(Of Corpse)
        Get
            Return CorpseData.ReadForFeature(Id).Select(Function(corpseId) New Corpse(corpseId))
        End Get
    End Property

    ReadOnly Property QuestGiver As QuestGiver
        Get
            Return QuestGiver.FromId(QuestData.ReadForFeatureId(Id))
        End Get
    End Property

    ReadOnly Property Entrance As Entrance
        Get
            Return Game.Entrance.FromId(EntranceData.ReadForFeatureId(Id))
        End Get
    End Property

    ReadOnly Property Egress As Egress
        Get
            Return Game.Egress.FromId(EgressData.ReadForFeatureId(Id))
        End Get
    End Property

    ReadOnly Property FullName As String
        Get
            Return FeatureType.FullName(Me)
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
