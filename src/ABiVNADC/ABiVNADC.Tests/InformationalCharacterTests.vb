<Collection("One Big Collection")>
Public Class InformationalCharacterTests
    Public Sub New()
        Store.Reset()
        MainProcessor.Run(DummyPlayer, "create character test")
    End Sub
    <Theory>
    <InlineData("inventory")>
    <InlineData("status")>
    Public Sub ShouldReturnInformationAndNotExplode(command As String)
        MainProcessor.Run(DummyPlayer, command).ShouldNotBeEmpty
    End Sub
End Class
