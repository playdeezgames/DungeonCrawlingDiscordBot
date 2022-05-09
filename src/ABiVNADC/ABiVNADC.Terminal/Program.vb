Imports System

Module Program
    Const DATABASE_FILE_NAME = "local.db"
    Sub Main(args As String())
        Store.Load(DATABASE_FILE_NAME)
        Dim player As New Player(AnsiConsole.Ask(Of Long)("[olive]Player Id[/]"))
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim command = AnsiConsole.Ask(Of String)(">")
            Select Case command
                Case "!quit"
                    done = True
                Case "!reset"
                    Store.Reset()
                Case "!save"
                    Store.Save(DATABASE_FILE_NAME)
                Case "!load"
                    Store.Load(DATABASE_FILE_NAME)
                Case Else
                    AnsiConsole.WriteLine()
                    AnsiConsole.WriteLine(MainProcessor.Run(player, command))
            End Select
        End While
        Store.Save(DATABASE_FILE_NAME)
    End Sub
End Module
