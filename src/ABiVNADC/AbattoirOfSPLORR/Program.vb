Imports System

Module Program
    Const DATABASE_FILE_NAME = "local.db"
    Const FIXED_PLAYER_ID As Long = 1
    Sub Main(args As String())
        Console.Title = "Abattoir of SPLORR!!"
        AnsiConsole.Clear()
        AnsiConsole.WriteLine("Welcome to:")
        Dim figlet = New FigletText("Abattoir of SPLORR!!").Centered()
        figlet.Color = Color.Red
        AnsiConsole.Write(figlet)
        AnsiConsole.MarkupLine("To get going: [olive]start[/], [olive]move[/], [olive]left[/], [olive]right[/], [olive]around[/]")
        AnsiConsole.MarkupLine("For General Commands: [olive]help[/]")
        AnsiConsole.MarkupLine("Special Commands: [olive]!quit[/](you will lose all progress if you don't use this), [olive]!reset[/], [olive]!save[/], [olive]!load[/]")
        Store.Load(DATABASE_FILE_NAME)
        Dim player As New Player(FIXED_PLAYER_ID)
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
