Module FeaturesProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("The command is just `features`.")
            Return
        End If
        RequireCharacter(
            player,
            builder,
            Sub(character)
                RequireLocation(
                    character,
                    builder,
                    Function(location)
                        Dim features = location.Features
                        If Not features.Any Then
                            Return $"There are no features here."
                        End If
                        Return $"Features present: {String.Join(", ", features.Select(Function(x) x.Name))}"
                    End Function)
            End Sub)
    End Sub
End Module
