Public Class Sprite
    ReadOnly Property SourceCanvas As TextCanvas
    ReadOnly Property DestinationColumn As Integer
    ReadOnly Property DestinationRow As Integer
    ReadOnly Property TransparentChar As Char
    Sub New(sourceCanvas As TextCanvas, destinationColumn As Integer, destinationRow As Integer, Optional transparentChar As Char = ChrW(0))
        Me.SourceCanvas = sourceCanvas
        Me.DestinationColumn = destinationColumn
        Me.DestinationRow = destinationRow
        Me.TransparentChar = transparentChar
    End Sub
    Sub Render(destinationCanvas As TextCanvas)
        destinationCanvas.Render(DestinationColumn, DestinationRow, SourceCanvas, TransparentChar)
    End Sub
End Class
