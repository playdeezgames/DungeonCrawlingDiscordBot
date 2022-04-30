Imports Xunit

Namespace ABiVNADC.Tests
    Public Class UnknownCommandTests
        <Fact>
        Sub ShouldReturnAMessageAndNotExplode()
            Store.Reset()
            Dim player As New Player(DummyPlayerId)
            Dim actual = MainProcessor.Run(player, "eat melba toast")
            actual.ShouldNotBeEmpty()
        End Sub
    End Class
    Public Class AboutCommandTests
        <Fact>
        Sub ShouldReturnAMessageAndNotExplode()
            Store.Reset()
            Dim player As New Player(DummyPlayerId)
            Dim actual = MainProcessor.Run(player, "about")
            actual.ShouldNotBeEmpty()
        End Sub
    End Class
    Public Class HelpCommandTests
        <Fact>
        Sub ShouldReturnAMessageAndNotExplode()
            Store.Reset()
            Dim player As New Player(DummyPlayerId)
            Dim actual = MainProcessor.Run(player, "help")
            actual.ShouldNotBeEmpty()
        End Sub
    End Class
End Namespace

