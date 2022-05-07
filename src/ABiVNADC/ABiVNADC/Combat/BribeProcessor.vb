Module BribeProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return "Bribe with what? Empty handed bribes are rude!"
        End If
        Dim itemTypeName = StitchTokens(tokens)
        Dim itemType = ParseItemType(itemTypeName)
        If itemType = ItemType.None Then
            Return $"I don't know what a `{itemTypeName}` is."
        End If
        Return RequireCharacter(
            player,
            Function(character)
                Return RequireLocation(
                    character,
                    Function(location)
                        If Not character.Inventory.StackedItems.Keys.Contains(itemType) Then
                            Return $"{character.FullName} doesn't have any {itemType.Name}."
                        End If
                        Dim enemy As Character = character.Location.Enemies(character).FirstOrDefault(Function(x) x.TakesBribe(itemType))
                        If enemy Is Nothing Then
                            Return $"No enemy in this location will take that."
                        End If
                        Return DoCounterAttacks(character, character.BribeEnemy(enemy, itemType))
                    End Function)
            End Function)
    End Function
End Module
