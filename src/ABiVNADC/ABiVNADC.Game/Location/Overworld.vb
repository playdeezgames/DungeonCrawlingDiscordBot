Public Class Overworld
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    ReadOnly Property X As Long
        Get
            Return OverworldLocationData.ReadX(Id).Value
        End Get
    End Property
    ReadOnly Property Location As Location
        Get
            Return New Location(Id)
        End Get
    End Property

    Friend Sub IncreasePeril()
        Dim peril = OverworldLocationData.ReadPeril(Id).Value + RNG.RollDice("1d2/2")
        Dim perilThreshold = OverworldLocationData.ReadPerilThreshold(Id).Value
        If peril >= perilThreshold Then
            TerrainType.GenerateWanderingMonster(Location)
            peril -= perilThreshold
        End If
        OverworldLocationData.WritePeril(Id, peril)
    End Sub

    ReadOnly Property TerrainType As TerrainType
        Get
            Return CType(OverworldLocationData.ReadTerrainType(Id).Value, TerrainType)
        End Get
    End Property

    ReadOnly Property Y As Long
        Get
            Return OverworldLocationData.ReadY(Id).Value
        End Get
    End Property

    Property ForageDepletion As Long
        Get
            Return OverworldLocationData.ReadForageDepletion(Id).Value
        End Get
        Set(value As Long)
            OverworldLocationData.WriteForageDeplection(Id, value)
        End Set
    End Property

    Friend Function GenerateForage() As Dictionary(Of ItemType, Long)
        Dim result = TerrainType.GenerateForage(ForageDepletion)
        ForageDepletion += result.Count
        Return result
    End Function
End Class
