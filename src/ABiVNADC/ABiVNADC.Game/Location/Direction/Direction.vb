Imports System.Runtime.CompilerServices

Public Enum Direction
    North
    East
    South
    West
End Enum
Public Module DirectionExtensions
    <Extension>
    Public Function Name(direction As Direction) As String
        Return DirectionDescriptors(direction).Name
    End Function
    Friend Function AllCardinalDirections() As IEnumerable(Of Direction)
        Return DirectionDescriptors.Where(Function(x) x.Value.IsCardinal).Select(Function(x) x.Key)
    End Function
    Friend Function DirectionWalker() As Dictionary(Of Direction, MazeDirection(Of Direction))
        Return DirectionDescriptors.
            Where(Function(x) x.Value.Walker IsNot Nothing).
            ToDictionary(Function(x) x.Key, Function(x) x.Value.Walker)
    End Function
    <Extension()>
    Public Function LeftDirection(direction As Direction) As Direction
        Return DirectionDescriptors(direction).LeftDirection.Value
    End Function
    <Extension()>
    Public Function RightDirection(direction As Direction) As Direction
        Return DirectionDescriptors(direction).RightDirection.Value
    End Function
    <Extension()>
    Public Function OppositeDirection(direction As Direction) As Direction
        Return DirectionDescriptors(direction).OppositeDirection.Value
    End Function
End Module