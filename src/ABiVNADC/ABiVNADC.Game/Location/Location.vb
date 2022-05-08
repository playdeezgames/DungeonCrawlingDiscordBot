Public Class Location
    Implements IInventoryHost
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
    End Sub

    ReadOnly Property IsPOV As Boolean
        Get
            Return LocationType.IsPOV
        End Get
    End Property

    ReadOnly Property LocationType As LocationType
        Get
            Return CType(LocationData.ReadLocationType(Id).Value, LocationType)
        End Get
    End Property

    ReadOnly Property Routes As Dictionary(Of Direction, Route)
        Get
            Dim result As New Dictionary(Of Direction, Route)
            For Each route In RouteData.ReadForForLocation(Id).Select(Function(id) New Route(id))
                result(route.Direction) = route
            Next
            Return result
        End Get
    End Property

    ReadOnly Property Dungeon As Dungeon
        Get
            Return Dungeon.FromId(DungeonLocationData.ReadForLocation(Id))
        End Get
    End Property

    ReadOnly Property Features As IEnumerable(Of Feature)
        Get
            Return FeatureData.ReadForLocation(Id).Select(Function(id) New Feature(id))
        End Get
    End Property

    ReadOnly Property HasFeatures As Boolean
        Get
            Return FeatureData.ReadCountForLocation(Id) > 0
        End Get
    End Property

    ReadOnly Property Inventory As Inventory Implements IInventoryHost.Inventory
        Get
            Dim inventoryId As Long? = LocationInventoryData.ReadForLocation(Id)
            If Not inventoryId.HasValue Then
                inventoryId = InventoryData.Create()
                LocationInventoryData.Write(Id, inventoryId.Value)
            End If
            Return New Inventory(inventoryId.Value)
        End Get
    End Property

    ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return CharacterLocationData.ReadForLocation(Id).Select(Function(id) New Character(id))
        End Get
    End Property

    Function Enemies(forCharacter As Character) As IEnumerable(Of Character)
        Return Characters.Where(Function(x) x.IsEnemy <> forCharacter.IsEnemy)
    End Function

    Function Enemy(forCharacter As Character) As Character
        Return Enemies(forCharacter).FirstOrDefault
    End Function

    Function HasEnemies(forCharacter As Character) As Boolean
        Return Enemies(forCharacter).Any
    End Function
End Class
