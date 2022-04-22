Imports System.Runtime.CompilerServices

Public Enum Direction
    North
    East
    South
    West
End Enum
Public Module DirectionExtensions
    Friend ReadOnly AllDirections As New List(Of Direction) From
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        }
    Friend ReadOnly DirectionWalker As New Dictionary(Of Direction, MazeDirection(Of Direction)) From
        {
            {Direction.North, New MazeDirection(Of Direction)(Direction.South, 0, -1)},
            {Direction.East, New MazeDirection(Of Direction)(Direction.West, 1, 0)},
            {Direction.South, New MazeDirection(Of Direction)(Direction.North, 0, 1)},
            {Direction.West, New MazeDirection(Of Direction)(Direction.East, -1, 0)}
        }
    <Extension()>
    Public Function LeftDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.North
                Return Direction.West
            Case Direction.East
                Return Direction.North
            Case Direction.South
                Return Direction.East
            Case Direction.West
                Return Direction.South
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension()>
    Public Function RightDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.North
                Return Direction.East
            Case Direction.East
                Return Direction.South
            Case Direction.South
                Return Direction.West
            Case Direction.West
                Return Direction.North
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module