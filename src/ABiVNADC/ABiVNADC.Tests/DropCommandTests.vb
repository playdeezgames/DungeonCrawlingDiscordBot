<Collection("One Big Collection")>
Public Class DropCommandTests
    Sub New()
        Store.Reset()
        MainProcessor.Run(DummyPlayer, "create character test")
        MainProcessor.Run(DummyPlayer, "create dungeon yermom test")
        MainProcessor.Run(DummyPlayer, "enter test")
        DummyPlayer.Character.Inventory.Add(Item.Create(ItemType.Jools))
    End Sub
    <Fact>
    Public Sub ShouldDropJools()
        Dim originalGroundItemCount = DummyPlayer.Character.Location.Inventory.Items.Count
        MainProcessor.Run(DummyPlayer, "drop jools").ShouldNotBeEmpty
        DummyPlayer.Character.Inventory.IsEmpty.ShouldBeTrue
        DummyPlayer.Character.Location.Inventory.Items.Count.ShouldBeGreaterThan(originalGroundItemCount)
    End Sub
    <Fact>
    Public Sub ShouldNotDropAnItemTheCharacterDoesNotHave()
        Dim originalGroundItemCount = DummyPlayer.Character.Location.Inventory.Items.Count
        MainProcessor.Run(DummyPlayer, "drop food").ShouldNotBeEmpty
        DummyPlayer.Character.Inventory.IsEmpty.ShouldBeFalse
        DummyPlayer.Character.Location.Inventory.Items.Count.ShouldBe(originalGroundItemCount)
    End Sub
End Class
