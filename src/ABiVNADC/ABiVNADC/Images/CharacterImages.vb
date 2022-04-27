Imports System.Runtime.CompilerServices

Module CharacterImages
    Private ReadOnly Goblin As New TextCanvas(Enhance(New List(Of String) From
                                            {
                                                "............",
                                                "............",
                                                "............",
                                                "............",
                                                ".....@@@....",
                                                "..... @ @...",
                                                ".....@@@....",
                                                ".........@..",
                                                ".....@.@@.@.",
                                                "....@..@@.@.",
                                                "......@.@...",
                                                ".....@@..@.."
                                            }))
    Private ReadOnly BossFish As New TextCanvas(Enhance(New List(Of String) From
                                            {
                                                "....@@@.....",
                                                ".....@@@....",
                                                ".....@@@@...",
                                                "@...@@@@@@..",
                                                "@..@@@@@@@@.",
                                                "@@@@@@@@@ @@",
                                                "@@@@@@@@@@@@",
                                                "@..@@@@@@@@.",
                                                "@...@@@@@@..",
                                                ".....@@@@...",
                                                ".....@@@....",
                                                "....@@@....."
                                            }))
    Private ReadOnly MinionFish As New TextCanvas(Enhance(New List(Of String) From
                                            {
                                                "............",
                                                "............",
                                                "............",
                                                ".....@@.....",
                                                "......@@....",
                                                "..@..@@ @...",
                                                "...@@@@@@...",
                                                "..@...@@....",
                                                ".....@@.....",
                                                "............",
                                                "............",
                                                "............"
                                            }))
    Private ReadOnly Orc As New TextCanvas(Enhance(New List(Of String) From
                                            {
                                                "............",
                                                "............",
                                                "............",
                                                "....@@@.@...",
                                                ".... @ @....",
                                                "....@@@.....",
                                                ".......@@...",
                                                ".....@@@.@..",
                                                "...@.@@@.@..",
                                                ".....@@@....",
                                                ".....@.@....",
                                                "....@@.@...."
                                            }))
    Private ReadOnly Zombie As New TextCanvas(Enhance(New List(Of String) From
                                            {
                                                "............",
                                                "............",
                                                "....@@@@....",
                                                ".... @  @...",
                                                "....@@@@@...",
                                                "....@.@.....",
                                                "........@...",
                                                ".....@@@.@..",
                                                "...@.@@@.@..",
                                                ".....@@@....",
                                                ".....@.@....",
                                                "....@@.@...."
                                            }))
    Private ReadOnly Skeleton As New TextCanvas(Enhance(New List(Of String) From
                                            {
                                                "............",
                                                ".....@@@....",
                                                "....@@@@@...",
                                                ".... @ @@...",
                                                "....@@@@....",
                                                "....@.@.....",
                                                "........@...",
                                                ".....@@@.@..",
                                                "...@..@..@..",
                                                ".....@@@....",
                                                ".....@.@....",
                                                "....@@.@...."
                                            }))
    Private Function Enhance(lines As List(Of String)) As List(Of String)
        Dim result As New List(Of String)
        For Each line In lines
            Dim enhancedLine = EnhanceLine(line)
            result.Add(enhancedLine)
            result.Add(enhancedLine)
        Next
        Return result
    End Function

    Private Function EnhanceLine(line As String) As String
        Dim builder As New StringBuilder
        For Each character In line
            builder.Append(character)
            builder.Append(character)
        Next
        Return builder.ToString
    End Function
    <Extension>
    Function Image(characterType As CharacterType) As TextCanvas
        Select Case characterType
            Case CharacterType.Goblin
                Return Goblin
            Case CharacterType.Orc
                Return Orc
            Case CharacterType.Skeleton
                Return Skeleton
            Case CharacterType.Zombie
                Return Zombie
            Case CharacterType.MinionFish
                Return MinionFish
            Case CharacterType.BossFish
                Return BossFish
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
