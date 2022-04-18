Module HelpProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "I don't know how to help that."
        Else
            Return "Commands: 
- help : you are here
- status : shows current status"
        End If
    End Function
End Module
