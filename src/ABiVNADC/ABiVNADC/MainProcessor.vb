﻿Module MainProcessor
    Const HelpText = "help"
    Function Run(player As Player, command As String) As String
        Dim tokens = command.Split(" "c)
        Select Case tokens.First.ToLower
            Case HelpText
                Return HelpProcessor.Run(player, tokens.Skip(1))
            Case Else
                Return "Dunno what you mean. Mebbe you need to try `help`?"
        End Select
    End Function
End Module
