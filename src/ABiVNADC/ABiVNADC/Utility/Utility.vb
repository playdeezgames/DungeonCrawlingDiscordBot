Module Utility

    Sub RequireNoCharacter(player As Player, builder As StringBuilder, handler As Action)
        If player.HasCharacter Then
            builder.AppendLine("You already have a character.")
            Return
        End If
        handler()
    End Sub
    Sub RequireCharacter(player As Player, builder As StringBuilder, handler As Action(Of Character))
        If Not player.HasCharacter Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        handler(player.Character)
    End Sub

    Sub RequireLocation(character As Character, builder As StringBuilder, handler As Action(Of Location))
        If Not character.HasLocation Then
            builder.AppendLine($"{character.FullName} does not have a location!")
            Return
        End If
        handler(character.Location)
    End Sub

    Sub RequireCharacterLocation(player As Player, builder As StringBuilder, handler As Action(Of Character, Location))
        RequireCharacter(
            player,
            builder,
            Sub(character)
                RequireLocation(
                    character,
                    builder,
                    Sub(location)
                        handler(character, location)
                    End Sub)
            End Sub)
    End Sub

    Sub RequireNoTokens(tokens As IEnumerable(Of String), commandName As String, builder As StringBuilder, handler As Action)
        If tokens.Any Then
            builder.AppendLine($"Round here, just `{commandName}` is the command. I'm picky.")
            Return
        End If
        handler()
    End Sub

    Sub RequireTokens(tokens As IEnumerable(Of String), errorMessage As String, builder As StringBuilder, handler As Action)
        If Not tokens.Any Then
            builder.AppendLine(errorMessage)
            Return
        End If
        handler()
    End Sub

    Sub RequireItemTypeQuantity(
                               tokens As IEnumerable(Of String),
                               builder As StringBuilder,
                               handler As Action(Of ItemType, Long))
        Dim quantity As Long = 1
        Dim itemTypeName As String
        If Long.TryParse(tokens.First, quantity) Then
            itemTypeName = StitchTokens(tokens.Skip(1))
        Else
            itemTypeName = StitchTokens(tokens)
            quantity = 1
        End If
        Dim itemType = ParseItemType(itemTypeName)
        If itemType = ItemType.None Then
            builder.AppendLine($"I don't know what a `{itemTypeName}` is.")
            Return
        End If
        handler(itemType, quantity)
    End Sub

    Sub RequireItemNameQuantity(tokens As IEnumerable(Of String), itemSource As Func(Of String, IEnumerable(Of Item)), builder As StringBuilder, handler As Action(Of IEnumerable(Of Item)))
        Dim quantity As Long = 1
        Dim itemTypeName As String
        If Long.TryParse(tokens.First, quantity) Then
            itemTypeName = StitchTokens(tokens.Skip(1))
        Else
            itemTypeName = StitchTokens(tokens)
            quantity = 1
        End If
        Dim items = itemSource(itemTypeName)
        If Not items.Any Then
            builder.AppendLine($"I don't see any `{itemTypeName}`.")
            Return
        End If
        handler(items.Take(CInt(quantity)))
    End Sub

    Sub RequireItemName(tokens As IEnumerable(Of String), itemSource As Func(Of String, IEnumerable(Of Item)), builder As StringBuilder, handler As Action(Of Item))
        Dim itemTypeName As String
        itemTypeName = StitchTokens(tokens)
        Dim items = itemSource(itemTypeName)
        If Not items.Any Then
            builder.AppendLine($"I don't see any `{itemTypeName}`.")
            Return
        End If
        handler(items.First)
    End Sub

    Sub RequireInCombat(character As Character, builder As StringBuilder, handler As Action)
        If Not character.InCombat Then
            builder.AppendLine($"{character.FullName} is not in combat.")
            Return
        End If
        handler()
    End Sub

    Sub ShowCurrentLocation(player As Player, builder As StringBuilder)
        Select Case If(player?.Character?.Location?.LocationType, LocationType.None)
            Case LocationType.Dungeon
                Dim canvas = DrawPOV(player)
                builder.AppendLine($"```{canvas.Output}```")
            Case LocationType.Overworld
                ShowOverworldLocation(player, player.Character.Location, builder)
            Case LocationType.Shoppe
                ShowShoppeLocation(player.Character, player.Character.Location.Shoppe, builder)
            Case LocationType.LandClaimOffice
                ShowLandClaimOffice(player.Character, builder)
            Case LocationType.IncentivesOffice
                ShowIncentivesOffice(player.Character, builder)
            Case Else
                builder.AppendLine("Cannot show current location.")
        End Select
    End Sub

    Private Sub ShowIncentivesOffice(character As Character, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} is in an Incentives Office.")
        If Not character.Location.Inventory.IsEmpty Then
            builder.AppendLine("There is stuff on the ground.")
        End If
    End Sub

    Private Sub ShowLandClaimOffice(character As Character, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} is in a Land Claim Office.")
        If Not character.Location.Inventory.IsEmpty Then
            builder.AppendLine("There is stuff on the ground.")
        End If
    End Sub

    Private Sub ShowShoppeLocation(character As Character, shoppe As Shoppe, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} is browsing {shoppe.Name}.")
        If Not character.Location.Inventory.IsEmpty Then
            builder.AppendLine("There is stuff on the ground.")
        End If
    End Sub

    Private Sub ShowOverworldLocation(player As Player, location As Location, builder As StringBuilder)
        builder.AppendLine(location.Overworld.TerrainType.Description(player.Character))
        Dim owner = location.Owner
        If owner IsNot Nothing Then
            builder.AppendLine($"This land is owned by {owner.FullName}.")
        End If
        If location.HasEnemies(player.Character) Then
            builder.AppendLine($"There are enemies about!")
        End If
        If location.HasFeatures Then
            For Each feature In location.Features
                builder.AppendLine($"There is {feature.FullName} here.")
            Next
        End If
        If Not location.Inventory.IsEmpty Then
            builder.AppendLine("There is stuff on the ground.")
        End If
    End Sub
End Module
