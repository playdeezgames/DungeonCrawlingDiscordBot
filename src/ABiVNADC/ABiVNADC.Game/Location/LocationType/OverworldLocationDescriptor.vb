Friend Class OverworldLocationDescriptor
    Inherits LocationTypeDescriptor
    Sub New()
        MyBase.New(False, False)
    End Sub
    Public Overrides Sub HandleEnteredBy(character As Character, location As Location)
        'increase peril
        'check for ambush
    End Sub
End Class
