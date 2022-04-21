Imports Discord.WebSocket
Imports Spectre.Console

Module Program
    Private Const DISCORD_TOKEN_ENVVAR = "DISCORD_TOKEN"
    Private Const DATABASE_FILE_NAME = "local.db"
    Sub Main(args As String())
        AnsiConsole.MarkupLine("[green]Starting...[/]")
        MainAsync().Wait()
        AnsiConsole.MarkupLine("[green]Stopping...[/]")
    End Sub
    Async Function MainAsync() As Task
        Using client As New DiscordSocketClient
            AddHandler client.Log, AddressOf Log
            AddHandler client.MessageReceived, AddressOf MessageReceived
            Dim token = Environment.GetEnvironmentVariable(DISCORD_TOKEN_ENVVAR)
            Store.Load(DATABASE_FILE_NAME)
            Await client.LoginAsync(TokenType.Bot, token)
            Await client.StartAsync()

            Dim done = False

            AddHandler Console.CancelKeyPress, Sub(sender As Object, e As ConsoleCancelEventArgs)
                                                   Store.Save(DATABASE_FILE_NAME)
                                                   done = True
                                               End Sub

            While Not done
                Await Task.Delay(1000)
            End While

            Await client.StopAsync()
        End Using
    End Function

    Private Async Function MessageReceived(arg As SocketMessage) As Task
        If Not arg.Author.IsBot Then
            If arg.Channel.GetChannelType = ChannelType.DM AndAlso Not String.IsNullOrWhiteSpace(arg.CleanContent) Then
                Dim response = MainProcessor.Run(New Player(CLng(arg.Author.Id)), arg.CleanContent)
                If Not String.IsNullOrWhiteSpace(response) Then
                    Await arg.Channel.SendMessageAsync(response)
                End If
            End If
        End If
    End Function

    Private Function Log(msg As LogMessage) As Task
        AnsiConsole.MarkupLine($"Log: [red]{msg.ToString().EscapeMarkup}[/]")
        Return Task.CompletedTask
    End Function
End Module
