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
                        If character.HasQuest Then
                            builder.AppendLine($"You already have a quest!")
                            Dim questGiver = character.QuestGiver
                            builder.AppendLine($"{questGiver.Name} wants you to find {questGiver.TargetQuantity} {questGiver.TargetItemType.Name}, and will reward you with {questGiver.RewardQuantity} {questGiver.RewardItemType.Name}.")
                            Return
                        End If
                        If location.HasFeature(FeatureType.QuestGiver) Then
                            Dim questGiver = location.QuestGiver
                            builder.AppendLine($"{questGiver.Name} wants you to find {questGiver.TargetQuantity} {questGiver.TargetItemType.Name}, and will reward you with {questGiver.RewardQuantity} {questGiver.RewardItemType.Name}.")
                        End If
                    End Sub)
            End Sub)
    End Sub
End Module
