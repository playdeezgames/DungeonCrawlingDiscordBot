Imports System.IO
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
            If arg.Channel.GetChannelType = ChannelType.DM Then
                AnsiConsole.MarkupLine($"{arg.Type}")
                Await arg.Channel.SendMessageAsync("```
##                                                    ##
  ##                                                ##
    ##                                            ##
      ##                                        ##
        ##                                    ##
          ##                                ##
            ##                            ##
              ############################
              #                          #
              #                          #
              #                          #
              #       ############       #
              #       #          #       #
              #       #          #       #
              #       #          #       #
              #       #          #       #
              #       #          #       #
              #       #          #       #
              #       #          #       #
              #       #          #       #
              ############################
            ##                            ##
          ##                                ##
        ##                                    ##
      ##                                        ##
    ##                                            ##
  ##                                                ##
##                                                    ##
```")
            End If
        End If
    End Function

    Private Function Log(msg As LogMessage) As Task
        AnsiConsole.MarkupLine($"Log: [red]{msg}[/]")
        Return Task.CompletedTask
    End Function
End Module
