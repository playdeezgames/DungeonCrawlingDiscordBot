<Collection("One Big Collection")>
Public Class UnknownCommandTests
    <Fact>
    Sub ShouldReturnAMessageAndNotExplode()
        Store.Reset()
        Dim actual = MainProcessor.Run(DummyPlayer, "eat melba toast")
        actual.ShouldNotBeEmpty()
    End Sub
End Class
Public Class AboutCommandTests
    <Fact>
    Sub ShouldReturnAMessageAndNotExplode()
        Store.Reset()
        Dim actual = MainProcessor.Run(DummyPlayer, "about")
        actual.ShouldNotBeEmpty()
    End Sub
End Class
Public Class HelpCommandTests
    <Fact>
    Sub ShouldReturnAMessageAndNotExplode()
        Store.Reset()
        Dim actual = MainProcessor.Run(DummyPlayer, "help")
        actual.ShouldNotBeEmpty()
    End Sub
End Class

