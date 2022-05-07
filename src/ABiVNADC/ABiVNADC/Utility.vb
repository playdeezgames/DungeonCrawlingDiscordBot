Module Utility
    Function RequireCharacter(player As Player, handler As Func(Of Character, String)) As String
        If Not player.HasCharacter Then
            Return "You have no current character."
        End If
        Return handler(player.Character)
    End Function

    Function RequireLocation(character As Character, handler As Func(Of Location, String)) As String
        If Not character.HasLocation Then
            Return $"{character.FullName} is not in a dungeon!"
        End If
        Return handler(character.Location)
    End Function
End Module
