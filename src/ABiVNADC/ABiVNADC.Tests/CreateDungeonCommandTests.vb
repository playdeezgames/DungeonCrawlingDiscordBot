﻿<Collection("One Big Collection")>
Public Class CreateDungeonCommandTests
    <Theory>
    <InlineData("yermom")>
    <InlineData("easy")>
    <InlineData("normal")>
    <InlineData("difficult")>
    <InlineData("too")>
    Public Sub ShouldCreateADungeon(size As String)
        Store.Reset()
        MainProcessor.Run(DummyPlayer, $"create dungeon {size} test")
        DummyPlayer.Dungeons.Count.ShouldBe(1)
    End Sub
    <Fact>
    Public Sub ShouldPreventDuplicateNamedDungeons()
        Store.Reset()
        MainProcessor.Run(DummyPlayer, $"create dungeon yermom test")
        MainProcessor.Run(DummyPlayer, $"create dungeon yermom test")
        DummyPlayer.Dungeons.Count.ShouldBe(1)
    End Sub
End Class
