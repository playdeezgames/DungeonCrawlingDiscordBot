<Collection("One Big Collection")>
Public Class NavigationNonCombatInPlayTests
    Public Sub New()
        Store.Reset()
        MainProcessor.Run(DummyPlayer, "create character test")
        MainProcessor.Run(DummyPlayer, "create dungeon yermom test")
        MainProcessor.Run(DummyPlayer, "enter test")
        Dim character = DummyPlayer.Character
        For Each enemy In DummyPlayer.Character.Location.Enemies(character)
            enemy.Destroy()
        Next
    End Sub
    <Theory>
    <InlineData("around")>
    <InlineData("left")>
    <InlineData("right")>
    Public Sub ShouldAllowNavigation(command As String)
        MainProcessor.Run(DummyPlayer, command).ShouldNotBeEmpty
    End Sub
End Class
