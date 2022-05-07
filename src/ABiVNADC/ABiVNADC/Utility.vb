Module Utility
    Function RequireCharacter(player As Player, handler As Func(Of Character, String)) As String
        If Not player.HasCharacter Then
            Return "You have no current character."
        End If
        Return handler(player.Character)
    End Function
End Module
