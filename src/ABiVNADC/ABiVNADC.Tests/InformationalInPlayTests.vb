<Collection("One Big Collection")>
Public Class InformationalInPlayTests
    Public Sub New()
        Store.Reset()
        MainProcessor.Run(DummyPlayer, "create character test")
        MainProcessor.Run(DummyPlayer, "create dungeon yermom test")
        MainProcessor.Run(DummyPlayer, "enter test")
    End Sub
    <Theory>
    <InlineData("enemies")>
    <InlineData("ground")>
    <InlineData("look")>
    Public Sub ShouldReturnInformationAndNotExplode(command As String)
        MainProcessor.Run(DummyPlayer, command).ShouldNotBeEmpty
    End Sub
End Class
