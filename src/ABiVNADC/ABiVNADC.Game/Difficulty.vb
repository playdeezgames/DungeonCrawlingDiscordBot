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
End Module
