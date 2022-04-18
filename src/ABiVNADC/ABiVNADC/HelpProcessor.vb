Module HelpProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Dim topic = String.Join(" "c, tokens)
            Select Case topic
                Case $"{CreateText}"
                    Return $"Create Command:
- create character (name) : creates a character with that name. no duplicates"
                Case Else
                    Return "I don't know how to help that."
            End Select
        Else
            Return "Commands: 
- help : you are here
- status : shows current status
- characters : shows list of characters
- create : creates something(use `help create` for more)"
        End If
    End Function
End Module
