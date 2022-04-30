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
    Public Sub ShouldAllowTurning(command As String)
        MainProcessor.Run(DummyPlayer, command).ShouldNotBeEmpty
    End Sub
    <Fact>
    Public Sub ShouldAllowMoving()
        Dim originalLocationId = DummyPlayer.Character.Location.Id
        DummyPlayer.SetDirection(DummyPlayer.Character.Location.Routes.Keys.First)
        MainProcessor.Run(DummyPlayer, "move").ShouldNotBeEmpty
        DummyPlayer.Character.Location.Id.ShouldNotBe(originalLocationId)
    End Sub
End Class
