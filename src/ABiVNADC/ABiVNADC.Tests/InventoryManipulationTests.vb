<Collection("One Big Collection")>
Public Class InventoryManipulationTests
    Public Sub New()
        Store.Reset()
        MainProcessor.Run(DummyPlayer, "create character test")
        MainProcessor.Run(DummyPlayer, "create dungeon yermom test")
        MainProcessor.Run(DummyPlayer, "enter test")
        DummyPlayer.Character.Location.Inventory.Add(Item.Create(ItemType.Jools))
        DummyPlayer.Character.Location.Inventory.Add(Item.Create(ItemType.Food))
    End Sub
    <Fact>
    Public Sub TakeAllCommandShouldTakeStuffFromTheGround()
        DummyPlayer.Character.Inventory.IsEmpty.ShouldBeTrue
        DummyPlayer.Character.Inventory.Items.ShouldBeEmpty
        DummyPlayer.Character.Location.Inventory.Items.ShouldNotBeEmpty
        MainProcessor.Run(DummyPlayer, "take all").ShouldNotBeEmpty
        DummyPlayer.Character.Inventory.IsEmpty.ShouldBeFalse
        DummyPlayer.Character.Inventory.Items.ShouldNotBeEmpty
        DummyPlayer.Character.Location.Inventory.Items.ShouldBeEmpty
    End Sub
    <Fact>
    Public Sub TakeJoolsCommandShouldTakeStuffFromTheGround()
        DummyPlayer.Character.Inventory.IsEmpty.ShouldBeTrue
        DummyPlayer.Character.Inventory.Items.ShouldBeEmpty
        Dim originalGroundItemCount = DummyPlayer.Character.Location.Inventory.Items.Count
        originalGroundItemCount.ShouldBeGreaterThan(0)
        MainProcessor.Run(DummyPlayer, "take jools").ShouldNotBeEmpty
        DummyPlayer.Character.Inventory.IsEmpty.ShouldBeFalse
        DummyPlayer.Character.Inventory.Items.ShouldNotBeEmpty
        DummyPlayer.Character.Location.Inventory.Items.Count.ShouldBeLessThan(originalGroundItemCount)
    End Sub
End Class
