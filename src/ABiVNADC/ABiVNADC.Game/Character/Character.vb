Imports System.Text

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    ReadOnly Property Name As String
        Get
            Return Data.CharacterData.ReadName(Id)
        End Get
    End Property
    ReadOnly Property HasLocation As Boolean
        Get
            Return Location IsNot Nothing
        End Get
    End Property

    Public Function RollAttack() As Long
        Return RNG.RollDice(CharacterType.AttackDice)
    End Function

    Public Function RollDefend() As Long
        Return RNG.RollDice(CharacterType.DefendDice)
    End Function

    ReadOnly Property IsEnemy As Boolean
        Get
            Return CharacterType.IsEnemy
        End Get
    End Property

    Public Sub AddWounds(damage As Long)
        CharacterData.WriteWounds(Id, Data.ReadWounds(Id).Value + damage)
    End Sub

    Shared Function FromId(characterId As Long?) As Character
        If characterId.HasValue Then
            Return New Character(characterId.Value)
        End If
        Return Nothing
    End Function

    Public ReadOnly Property IsDead() As Boolean
        Get
            Return Health <= 0
        End Get
    End Property

    Public Sub Destroy()
        If HasLocation Then
            For Each item In Inventory.Items
                Location.Inventory.Add(item)
            Next
        End If
        CharacterData.Clear(Id)
    End Sub

    Property Location As Location
        Get
            Dim locationId As Long? = CharacterLocationData.Read(Id)
            If locationId.HasValue Then
                Return New Location(locationId.Value)
            End If
            Return Nothing
        End Get
        Set(value As Location)
            If value Is Nothing Then
                CharacterLocationData.Clear(Id)
            Else
                CharacterLocationData.Write(Id, value.Id)
            End If
        End Set
    End Property
    ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId As Long? = CharacterInventoryData.ReadForCharacter(Id)
            If Not inventoryId.HasValue Then
                inventoryId = InventoryData.Create()
                CharacterInventoryData.Write(Id, inventoryId.Value)
            End If
            Return New Inventory(inventoryId.Value)
        End Get
    End Property
    ReadOnly Property CharacterType As CharacterType
        Get
            Return CType(ReadCharacterType(Id).Value, CharacterType)
        End Get
    End Property
    ReadOnly Property CanFight As Boolean
        Get
            Return If(Location?.HasEnemies, False) AndAlso Energy >= CharacterType.FightEnergyCost
        End Get
    End Property
    ReadOnly Property Health As Long
        Get
            Return MaximumHealth - ReadWounds(Id).Value
        End Get
    End Property
    ReadOnly Property Energy As Long
        Get
            Return MaximumEnergy - CharacterData.ReadFatigue(Id).Value
        End Get
    End Property
    ReadOnly Property Level As Long
        Get
            Return Data.ReadLevel(Id).Value
        End Get
    End Property
    ReadOnly Property MaximumHealth As Long
        Get
            Return CharacterType.MaximumHealth(Level)
        End Get
    End Property
    ReadOnly Property MaximumEnergy As Long
        Get
            Return CharacterType.MaximumEnergy(Level)
        End Get
    End Property
    Function Attack(defender As Character) As String
        Dim builder As New StringBuilder
        Dim fatigue = CharacterType.FightEnergyCost
        AddFatigue(fatigue)
        Dim attackRoll = RollAttack()
        builder.AppendLine($"{Name} rolls an attack of {attackRoll}!")
        Dim defendRoll = defender.RollDefend
        builder.AppendLine($"{defender.Name} rolls a defend of {defendRoll}!")
        Dim damageRoll = If(attackRoll > defendRoll, attackRoll - defendRoll, 0)
        If damageRoll > 0 Then
            defender.AddWounds(damageRoll)
            builder.AppendLine($"{Name} hits!")
            builder.AppendLine($"{defender.Name} takes {damageRoll} damage!")
        Else
            builder.AppendLine($"{Name} misses!")
        End If
        If defender.IsDead Then
            builder.AppendLine($"{Name} kills {defender.Name} (they had a family, you know!)")
            defender.Destroy()
        End If
        Return builder.ToString
    End Function

    Private Sub AddFatigue(fatigue As Long)
        CharacterData.WriteFatigue(Id, CharacterData.ReadFatigue(Id).Value + fatigue)
    End Sub
End Class
