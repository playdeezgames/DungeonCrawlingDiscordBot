Module FeaturesProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            FeaturesText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireLocation(
                            character,
                            builder,
                            Sub(location)
                                Dim features = location.Features
                                If Not features.Any Then
                                    builder.AppendLine($"There are no features here.")
                                    Return
                                End If
                                builder.AppendLine($"Features present: {String.Join(", ", features.Select(Function(x) x.FullName))}")
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
