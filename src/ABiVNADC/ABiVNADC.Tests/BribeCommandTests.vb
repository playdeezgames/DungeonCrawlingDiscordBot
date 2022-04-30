<Collection("One Big Collection")>
Public Class BribeCommandTests
    Public Sub New()
        Store.Reset()
        MainProcessor.Run(DummyPlayer, "create character test")
        MainProcessor.Run(DummyPlayer, "create dungeon yermom test")
        MainProcessor.Run(DummyPlayer, "enter test")
        Dim character = DummyPlayer.Character
        For Each enemy In DummyPlayer.Character.Location.Enemies(character)
            enemy.Destroy()
        Next
        Dim goblin As Character = character.Create(CharacterType.Goblin, 0)
        goblin.Location = character.Location
        character.Inventory.Add(Item.Create(ItemType.Food))
    End Sub
    <Fact>
    Public Sub ShouldBribeGoblinWithFood()
        DummyPlayer.Character.Inventory.IsEmpty.ShouldBeFalse
        DummyPlayer.Character.Location.Enemies(DummyPlayer.Character).ShouldNotBeEmpty
        MainProcessor.Run(DummyPlayer, "bribe food")
        DummyPlayer.Character.Location.Enemies(DummyPlayer.Character).ShouldBeEmpty
        DummyPlayer.Character.Inventory.IsEmpty.ShouldBeTrue
    End Sub
End Class
