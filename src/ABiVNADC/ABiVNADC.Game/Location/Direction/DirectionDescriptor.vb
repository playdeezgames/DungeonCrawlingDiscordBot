Public Class DirectionDescriptor
    Property Name As String
    Property IsCardinal As Boolean
    Property Walker As MazeDirection(Of Direction)
    Property LeftDirection As Direction?
    Property RightDirection As Direction?
    Property OppositeDirection As Direction?
    Sub New()
        IsCardinal = False
        Walker = Nothing
        LeftDirection = Nothing
        RightDirection = Nothing
        OppositeDirection = Nothing
    End Sub
End Class
Module DirectionDescriptorExtensions
    Friend ReadOnly DirectionDescriptors As New Dictionary(Of Direction, DirectionDescriptor) From
        {
            {
                Direction.North,
                New DirectionDescriptor With
                {
                    .Name = "north",
                    .IsCardinal = True,
                    .Walker = New MazeDirection(Of Direction)(Direction.South, 0, -1),
                    .LeftDirection = Direction.West,
                    .OppositeDirection = Direction.South,
                    .RightDirection = Direction.East
                }
            },
            {
                Direction.East,
                New DirectionDescriptor With
                {
                    .Name = "east",
                    .IsCardinal = True,
                    .Walker = New MazeDirection(Of Direction)(Direction.West, 1, 0),
                    .LeftDirection = Direction.North,
                    .OppositeDirection = Direction.West,
                    .RightDirection = Direction.South
                }
            },
            {
                Direction.South,
                New DirectionDescriptor With
                {
                    .Name = "south",
                    .IsCardinal = True,
                    .Walker = New MazeDirection(Of Direction)(Direction.North, 0, 1),
                    .LeftDirection = Direction.East,
                    .OppositeDirection = Direction.North,
                    .RightDirection = Direction.West
                }
            },
            {
                Direction.West,
                New DirectionDescriptor With
                {
                    .Name = "west",
                    .IsCardinal = True,
                    .Walker = New MazeDirection(Of Direction)(Direction.East, -1, 0),
                    .LeftDirection = Direction.South,
                    .OppositeDirection = Direction.East,
                    .RightDirection = Direction.North
                }
            },
            {
                Direction.Inward,
                New DirectionDescriptor With
                {
                    .Name = "in"
                }
            },
            {
                Direction.Outward,
                New DirectionDescriptor With
                {
                    .Name = "out"
                }
            }
        }
End Module
