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

    <Extension>
    Function Name(characterType As CharacterType) As String
        Dim result As String = Nothing
        nameTable.TryGetValue(characterType, result)
        Return result
    End Function

    <Extension>
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

    <Extension>
    Function MaximumEnergy(characterType As CharacterType, level As Long) As Long
        Select Case characterType
            Case CharacterType.N00b
                Return 7
            Case CharacterType.Goblin
                Return 10
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public ReadOnly AllCharacterTypes As New List(Of CharacterType) From
        {
            CharacterType.N00b,
            CharacterType.Goblin
        }

    <Extension>
    Public Function SpawnCount(characterType As CharacterType, locationCount As Long) As Long
        Select Case characterType
            Case CharacterType.Goblin
                Return locationCount \ 2
        End Select
        Return 0
    End Function

    <Extension>
    Public Function RandomName(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.Goblin
                Return RNG.FromList(Names.Goblin)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Public Function IsEnemy(characterType As CharacterType) As Boolean
        Select Case characterType
            Case CharacterType.Goblin
                Return True
            Case Else
                Return False
        End Select
    End Function

    <Extension>
    Public Function AttackDice(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.N00b
                Return "1d3/3"
            Case CharacterType.Goblin
                Return "1d3/3+1d3/3"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Public Function DefendDice(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.N00b
                Return "1d3/3+1d3/3"
            Case CharacterType.Goblin
                Return "1d6/6"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Public Function FightEnergyCost(characterType As CharacterType) As Long
        Select Case characterType
            Case CharacterType.N00b
                Return 4
            Case CharacterType.Goblin
                Return 6
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module