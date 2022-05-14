Module QuestProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            QuestText,
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        If location.HasFeature(FeatureType.QuestGiver) Then
                            Dim questGiver = location.QuestGiver
                            builder.AppendLine($"{questGiver.Name} wants you to find {questGiver.TargetQuantity} {questGiver.TargetItemType.Name}, and will reward you with {questGiver.RewardQuantity} {questGiver.RewardItemType.Name}.")
                        End If
                    End Sub)
            End Sub)
    End Sub
End Module
