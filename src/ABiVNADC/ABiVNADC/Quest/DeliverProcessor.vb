Module DeliverProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            DeliverText,
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        character.Deliver(builder)
                    End Sub)
            End Sub)
    End Sub
End Module
