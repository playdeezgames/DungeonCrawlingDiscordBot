Public Class Location
    Implements IInventoryHost
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
    End Sub

    ReadOnly Property TerrainType As TerrainType?
        Get
            Dim result = OverworldLocationData.ReadTerrainType(Id)
            Return If(result.HasValue, CType(result.Value, TerrainType), Nothing)
        End Get
    End Property

    ReadOnly Property IsInsideShoppe As Boolean
        Get
            Return Id = If(Shoppe?.InsideLocation?.Id, 0)
        End Get
    End Property

    Private Shared Function CreateOverworld(x As Long, y As Long, terrainType As TerrainType) As Location
        Dim location = New Location(LocationData.Create(LocationType.Overworld))
        OverworldLocationData.Write(location.Id, x, y, terrainType)
        If x = 0 AndAlso y = 0 Then
            GenerateCrossRoads(location)
        ElseIf x = 0 Then
            GenerateNorthSouthRoad(location)
        ElseIf y = 0 Then
            GenerateEastWestRoad(location)
        Else
            Dim featureType = RNG.FromGenerator(OverworldFeatureGenerator)
            Select Case featureType
                Case FeatureType.DungeonEntrance
                    GenerateDungeonEntrance(location)
                Case FeatureType.ForSaleSign
                    GenerateForSaleSign(location)
                Case FeatureType.ShoppeEntrance
                    GenerateShoppeEntrance(location)
                Case FeatureType.QuestGiver
                    GenerateQuestGiver(location)
                Case Else
                    Feature.Create(location, featureType)
            End Select
        End If
        Return location
    End Function

    Private Shared Sub GenerateQuestGiver(location As Location)
        Dim feature = Game.Feature.Create(location, FeatureType.QuestGiver)
        Dim targetItemType = RNG.FromGenerator(QuestTargetGenerator)
        Dim rewardItemType = RNG.FromGenerator(QuestRewardGenerator)
        Dim name = Names.GenerateQuestGiverName
        QuestGiver.Create(
            feature.Id,
            name,
            targetItemType,
            RNG.RollDice(targetItemType.QuestTargetQuantityDice),
            rewardItemType,
            RNG.RollDice(rewardItemType.QuestRewardQuantityDice))
    End Sub

    ReadOnly Property OverworldX As Long?
        Get
            Return OverworldLocationData.ReadX(Id)
        End Get
    End Property

    Public Function HasFeature(featureType As FeatureType) As Boolean
        Return Features.Any(Function(x) x.FeatureType = featureType)
    End Function

    ReadOnly Property QuestGiver As QuestGiver
        Get
            Return Features.SingleOrDefault(Function(x) x.FeatureType = FeatureType.QuestGiver)?.QuestGiver
        End Get
    End Property

    ReadOnly Property OverworldY As Long?
        Get
            Return OverworldLocationData.ReadY(Id)
        End Get
    End Property

    Private Shared Function FromExistingOverworldXY(x As Long, y As Long) As Location
        Dim locationId As Long? = OverworldLocationData.ReadForXY(x, y)
        Return If(locationId.HasValue, New Location(locationId.Value), Nothing)
    End Function

    Friend Shared Function AutogenerateOverworldXY(x As Long, y As Long) As Location
        Return If(FromExistingOverworldXY(x, y), CreateOverworld(x, y, RNG.FromGenerator(TerrainTypeGenerator)))
    End Function

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

    ReadOnly Property HasDungeon As Boolean
        Get
            Return DungeonLocationData.ReadForLocation(Id).HasValue
        End Get
    End Property

    ReadOnly Property Shoppe As Shoppe
        Get
            Return Shoppe.FromId(ShoppeLocationData.ReadForLocation(Id))
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

    Shared Function FromId(locationId As Long?) As Location
        Return If(locationId.HasValue, New Location(locationId.Value), Nothing)
    End Function
End Class
