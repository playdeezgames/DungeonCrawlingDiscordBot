Imports System
Imports System.IO

Module Program
    Sub Main(args As String())
#Disable Warning CA1416 ' Validate platform compatibility
        Dim bmp As Bitmap = Bitmap.FromFile("Original.png")
        Using output = File.CreateText("output.txt")
            For cellColumn = 0 To 7
                For cellRow = 0 To 15
                    Dim imageNumber = cellColumn * 16 + cellRow
                    Dim offsetX = cellColumn * 9
                    Dim offsetY = cellRow * 9
                    output.Write("(")
                    For pixelY = 0 To 7
                        Dim patternValue = 0
                        Dim patternBit = 1
                        If pixelY > 0 Then
                            output.Write(",")
                        End If
                        For pixelX = 0 To 7
                            Dim color = bmp.GetPixel(offsetX + pixelX, offsetY + pixelY)
                            If color.R = 0 Then
                                patternValue += patternBit
                            End If
                            patternBit *= 2
                        Next
                        output.Write(patternValue)
                    Next
                    output.WriteLine("),")
                Next
            Next
        End Using
#Enable Warning CA1416 ' Validate platform compatibility
    End Sub
End Module
