Imports System.Runtime.CompilerServices

Public Enum Difficulty
    None
    Yermom
    Easy
    Normal
    Difficult
    Too
End Enum
Public Module DifficultyExtensions
    Public ReadOnly AllDifficulties As New List(Of Difficulty) From
        {
            Difficulty.Yermom,
            Difficulty.Easy,
            Difficulty.Normal,
            Difficulty.Difficult,
            Difficulty.Too
        }
    Public Function ParseDifficulty(text As String) As Difficulty
        Return AllDifficulties.FirstOrDefault(Function(x) x.Name = text)
    End Function
    <Extension>
    Public Function Name(difficulty As Difficulty) As String
        Select Case difficulty
            Case Difficulty.Difficult
                Return "difficult"
            Case Difficulty.Easy
                Return "easy"
            Case Difficulty.Normal
                Return "normal"
            Case Difficulty.Too
                Return "too"
            Case Difficulty.Yermom
                Return "yermom"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private ReadOnly WanderingMonsterTables As New Dictionary(Of Difficulty, Dictionary(Of CharacterType, Integer)) From
        {
            {
                Difficulty.Yermom,
                New Dictionary(Of CharacterType, Integer) From
                {
                    {CharacterType.None, 1}
                }
            },
            {
                Difficulty.Easy,
                New Dictionary(Of CharacterType, Integer) From
                {
                    {CharacterType.None, 9},
                    {CharacterType.Goblin, 1}
                }
            },
            {
                Difficulty.Normal,
                New Dictionary(Of CharacterType, Integer) From
                {
                    {CharacterType.None, 6},
                    {CharacterType.Goblin, 1},
                    {CharacterType.Skeleton, 1}
                }
            },
            {
                Difficulty.Difficult,
                New Dictionary(Of CharacterType, Integer) From
                {
                    {CharacterType.None, 5},
                    {CharacterType.Goblin, 2},
                    {CharacterType.Skeleton, 2},
                    {CharacterType.Orc, 1}
                }
            },
            {
                Difficulty.Too,
                New Dictionary(Of CharacterType, Integer) From
                {
                    {CharacterType.None, 3},
                    {CharacterType.Goblin, 2},
                    {CharacterType.Skeleton, 2},
                    {CharacterType.Orc, 1},
                    {CharacterType.Zombie, 1}
                }
            }
        }

    <Extension>
    Public Function WanderingMonsterTable(difficulty As Difficulty) As Dictionary(Of CharacterType, Integer)
        Return WanderingMonsterTables(difficulty)
    End Function
End Module
