﻿Module HelpProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Dim topic = String.Join(" "c, tokens)
            Select Case topic
                Case $"{CreateText}"
                    Return $"Create Command:
- create character (name) : creates a character with that name. no duplicates
- create dungeon (name) : creates a dungeon with that name. no duplicates"
                Case Else
                    Return "I don't know how to help that."
            End Select
        Else
            Return "Commands: 
- around : turn around
- characters : shows list of characters
- dungeons : shows list of dungeons
- create : creates something(use `help create` for more)
- enter (dungeon name) : causes yer current character to enter the dungeon
- ground : lists items (if any) on the ground near you
- help : <== you are here
- left : turn left
- move : move ahead
- right : turn right
- status : shows current status
- switch character (name) : switches yer active character"
        End If
    End Function
End Module
