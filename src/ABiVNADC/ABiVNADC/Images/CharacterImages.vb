﻿Imports System.Runtime.CompilerServices

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
    Private ReadOnly Mummy As New TextCanvas(Enhance(New List(Of String) From
                                            {
                                                "............",
                                                ".....@@.....",
                                                "....@@@@....",
                                                "....   @....",
                                                "....@ @ @...",
                                                "....    @...",
                                                "....@  @@...",
                                                ".....@@@.@..",
                                                "...@.@@@.@..",
                                                ".....@@@....",
                                                ".....@@@....",
                                                "....@@@@@..."
                                            }))
    Private ReadOnly Crab As New TextCanvas(Enhance(New List(Of String) From
                                            {
                                                "............",
                                                "..@@@...@@@.",
                                                ".@@@.....@@@",
                                                ".@@.......@@",
                                                ".@@@.....@@@",
                                                ".@@..@@@..@@",
                                                "..@.@ @ @..@",
                                                "....@@@@@...",
                                                "..@.......@.",
                                                ".@.@@@@@@@.@",
                                                "...@@   @@..",
                                                "..@.@@@@@.@."
                                            }))

    Private ReadOnly GoblinSprite As New Sprite(Goblin, 16, 2, "."c)
    Private ReadOnly OrcSprite As New Sprite(Orc, 16, 2, "."c)
    Private ReadOnly SkeletonSprite As New Sprite(Skeleton, 16, 2, "."c)
    Private ReadOnly ZombieSprite As New Sprite(Zombie, 16, 2, "."c)
    Private ReadOnly MinionFishSprite As New Sprite(MinionFish, 16, 2, "."c)
    Private ReadOnly BossFishSprite As New Sprite(BossFish, 16, 2, "."c)
    Private ReadOnly MummySprite As New Sprite(Mummy, 16, 2, "."c)
    Private ReadOnly CrabSprite As New Sprite(Crab, 16, 2, "."c)
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
    Function Sprite(characterType As CharacterType) As Sprite
        Select Case characterType
            Case CharacterType.Goblin
                Return GoblinSprite
            Case CharacterType.Orc
                Return OrcSprite
            Case CharacterType.Skeleton
                Return SkeletonSprite
            Case CharacterType.Zombie
                Return ZombieSprite
            Case CharacterType.MinionFish
                Return MinionFishSprite
            Case CharacterType.BossFish
                Return BossFishSprite
            Case CharacterType.Mummy
                Return MummySprite
            Case CharacterType.Crab
                Return CrabSprite
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
