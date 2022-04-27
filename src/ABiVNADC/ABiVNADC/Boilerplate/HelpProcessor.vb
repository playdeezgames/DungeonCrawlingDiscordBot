Module HelpProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return TopicalHelp(String.Join(" "c, tokens))
        Else
            Return "Check out the GitHub Wiki!
https://github.com/playdeezgames/DungeonCrawlingDiscordBot/wiki"
        End If
    End Function

    Private Function TopicalHelp(topic As String) As String
        Select Case topic
            Case $"{CreateText}"
                Return $"Create Command:
- create character (name) : creates a character with that name. no duplicates
- create dungeon (difficulty) (name) : creates a dungeon with that name. no duplicates
    difficulty levels: yermom, easy, normal, difficult, too"
            Case $"{DeleteText}"
                Return $"Create Command:
- delete character (name) : creates a character with that name. no duplicates
- delete dungeon (name) : creates a dungeon with that name. no duplicates"
            Case Else
                Return "I don't know how to help that."
        End Select
    End Function
End Module
