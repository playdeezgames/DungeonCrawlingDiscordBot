<Collection("One Big Collection")>
Public Class InformationalPlayerCommandTests
    <Theory>
    <InlineData("eat melba toast")>
    <InlineData("about")>
    <InlineData("help")>
    <InlineData("characters")>
    <InlineData("dungeons")>
    Sub ShouldReturnAMessageAndNotExplode(command As String)
        Store.Reset()
        Dim actual = MainProcessor.Run(DummyPlayer, command)
        actual.ShouldNotBeEmpty()
    End Sub
End Class
