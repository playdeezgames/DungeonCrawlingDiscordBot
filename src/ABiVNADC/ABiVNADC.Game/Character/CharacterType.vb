Imports System.Runtime.CompilerServices

Public Enum CharacterType
    None
    N00b
    Goblin
End Enum
Public Module CharacterTypeExtensions
    Private ReadOnly nameTable As New Dictionary(Of CharacterType, String) From
        {
            {CharacterType.N00b, "n00b"},
            {CharacterType.Goblin, "goblin"}
        }

    <Extension()>
    Function Name(characterType As CharacterType) As String
        Dim result As String = Nothing
        nameTable.TryGetValue(characterType, result)
        Return result
    End Function

    <Extension()>
    Function MaximumHealth(characterType As CharacterType, level As Long) As Long
        Select Case characterType
            Case CharacterType.N00b
                Return 5
            Case CharacterType.Goblin
                Return 1
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module