Friend Class DungeonLocationDescriptor
    Inherits LocationTypeDescriptor
    Sub New()
        MyBase.New(True, True)
    End Sub
    Public Overrides Sub HandleEnteredBy(character As Character, location As Location)
        'do nothing
    End Sub
End Class
