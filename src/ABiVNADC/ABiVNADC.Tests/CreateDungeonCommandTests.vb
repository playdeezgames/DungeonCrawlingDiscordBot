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
        Store.ExecuteScalar(Of Long)("SELECT COUNT(1) FROM [Dungeons];").ShouldBe(1)
    End Sub
End Class
