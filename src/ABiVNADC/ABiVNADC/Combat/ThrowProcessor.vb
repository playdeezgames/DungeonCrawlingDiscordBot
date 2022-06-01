Module ThrowProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            ThrowText,
            builder,
            Sub()
                player.ThrowWeapon(builder)
            End Sub)
    End Sub
End Module
