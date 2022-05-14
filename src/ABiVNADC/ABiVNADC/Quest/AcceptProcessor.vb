Module AcceptProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            AcceptText,
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        If Not location.HasFeature(FeatureType.QuestGiver) Then
                            builder.AppendLine("There is no quest giver here!")
                        End If
                        character.AcceptQuest(location.QuestGiver, builder)
                    End Sub)
            End Sub)
    End Sub
End Module
