Public Module MainProcessor
    Function Run(player As Player, command As String) As String
        Dim tokens = command.Split(" "c)
        Select Case tokens.First.ToLower
            Case CharactersText
                Return CharactersProcessor.Run(player, tokens.Skip(1))
            Case CreateText
                Return CreateProcessor.Run(player, tokens.Skip(1))
            Case DungeonsText
                Return DungeonsProcessor.Run(player, tokens.Skip(1))
            Case EnterText
                Return EnterProcessor.Run(player, tokens.Skip(1))
            Case HelpText
                Return HelpProcessor.Run(player, tokens.Skip(1))
            Case StatusText
                Return StatusProcessor.Run(player, tokens.Skip(1))
            Case SwitchText
                Return SwitchProcessor.Run(player, tokens.Skip(1))
            Case Else
                Return "Dunno what you mean. Mebbe you need to try `help`?"
        End Select
    End Function
End Module
