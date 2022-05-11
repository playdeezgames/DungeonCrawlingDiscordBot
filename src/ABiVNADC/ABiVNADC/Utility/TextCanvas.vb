Public Class TextCanvas
    ReadOnly Property Columns As Long
    ReadOnly Property Rows As Long
    Private characters As New List(Of Char)
    Sub New(lines As IEnumerable(Of String))
        Rows = lines.Count
        Columns = lines.First.Length
        For row = 0 To Rows - 1
            For column = 0 To Columns - 1
                characters.Add(lines.Skip(CInt(row)).First()(CInt(column)))
            Next
        Next
    End Sub
    Sub New(columns As Long, rows As Long, character As Char)
        Me.Columns = columns
        Me.Rows = rows
        While characters.Count < columns * rows
            characters.Add(character)
        End While
    End Sub
    Function Output() As String
        Dim builder As New StringBuilder
        Dim counter As Integer = 0
        For Each character In characters
            builder.Append(character)
            counter += 1
            If counter Mod Columns = 0 Then
                builder.AppendLine()
            End If
        Next
        Return builder.ToString
    End Function

    Friend Sub Render(destinationColumn As Integer, destinationRow As Integer, source As TextCanvas, Optional transparent As Char = ChrW(0))
        For column = 0 To source.Columns - 1
            For row = 0 To source.Rows - 1
                Dim character = source.GetCell(column, row)
                If character.HasValue AndAlso character.Value <> transparent Then
                    SetCell(destinationColumn + column, destinationRow + row, source.GetCell(column, row))
                End If
            Next
        Next
    End Sub

    Private Sub SetCell(column As Long, row As Long, character As Char?)
        If character.HasValue AndAlso column >= 0 AndAlso column < Columns AndAlso row >= 0 AndAlso row < Rows Then
            characters(CInt(column + row * Columns)) = character.Value
        End If
    End Sub

    Private Function GetCell(column As Long, row As Long) As Char?
        If column >= 0 AndAlso column < Columns AndAlso row >= 0 AndAlso row < Rows Then
            Return characters(CInt(column + row * Columns))
        End If
        Return Nothing
    End Function
End Class
