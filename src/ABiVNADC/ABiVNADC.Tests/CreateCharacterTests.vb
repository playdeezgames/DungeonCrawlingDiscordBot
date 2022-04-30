<Collection("One Big Collection")>
Public Class CreateCharacterTests
    <Fact>
    Public Sub ShouldCreateACharacter()
        Store.Reset()
        DummyPlayer.Character.ShouldBeNull
        DummyPlayer.Characters.ShouldBeEmpty
        Const characterName = "test"
        MainProcessor.Run(DummyPlayer, $"create character {characterName}").ShouldNotBeEmpty
        DummyPlayer.Characters.Count.ShouldBe(1)
        DummyPlayer.Character.ShouldNotBeNull
        DummyPlayer.Character.Name.ShouldBe(characterName)
    End Sub
    <Fact>
    Public Sub ShouldNotCreateADuplicateCharacter()
        Store.Reset()
        DummyPlayer.Character.ShouldBeNull
        DummyPlayer.Characters.ShouldBeEmpty
        Const characterName = "test"
        MainProcessor.Run(DummyPlayer, $"create character {characterName}").ShouldNotBeEmpty
        MainProcessor.Run(DummyPlayer, $"create character {characterName}").ShouldNotBeEmpty
        DummyPlayer.Characters.Count.ShouldBe(1)
        DummyPlayer.Character.ShouldNotBeNull
        DummyPlayer.Character.Name.ShouldBe(characterName)
    End Sub
End Class
