Module HelpProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return TopicalHelp(String.Join(" "c, tokens))
        Else
            Return "Commands: 
- about : tells you about this game
- around : turn around
- characters : shows list of characters
- create : creates something(use `help create` for more)
- delete : deletest something(use `help delete` for more)
- drop (item name) : drops an item from yer current charactor to the ground
- dungeons : shows list of dungeons
- enemies : shows a list of enemies faced by the current character
- enter (dungeon name) : causes yer current character to enter the dungeon
- equip (item) : causes yer current 
- ground : lists items (if any) on the ground near you
- help : <== you are here
- inventory : lists items in yer current character's inventory
- left : turn left
- look : shows the current POV for the current character
- move : move ahead
- rest : recover energy
- right : turn right
- status : shows current character's status
- switch character (name) : switches yer active character
- take all : yer current character picks up all of the items from the ground
- take (item name) : yer current character picks up an item from the ground
- use (item name) : yer current character uses an item"
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
