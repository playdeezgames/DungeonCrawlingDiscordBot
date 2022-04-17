Imports Discord.WebSocket
Imports Spectre.Console

Module Program
    Private Const DISCORD_TOKEN_ENVVAR = "DISCORD_TOKEN"
    Sub Main(args As String())
        MainAsync.Wait()
    End Sub
    Async Function MainAsync() As Task
        Dim client As New DiscordSocketClient
        AddHandler client.Log, AddressOf Log
        AddHandler client.MessageReceived, AddressOf MessageReceived
        Dim token = Environment.GetEnvironmentVariable(DISCORD_TOKEN_ENVVAR)

        Await client.LoginAsync(TokenType.Bot, token)
        Await client.StartAsync()

        Await Task.Delay(-1)
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
        AnsiConsole.MarkupLine($"Log: [red]{msg}[/]")
        Return Task.CompletedTask
    End Function
End Module
