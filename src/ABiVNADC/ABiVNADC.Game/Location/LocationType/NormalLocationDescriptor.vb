Friend Class NormalLocationDescriptor
    Inherits LocationTypeDescriptor
    Sub New()
        MyBase.New(False, False)
    End Sub
    Public Overrides Sub HandleEnteredBy(character As Character, location As Location)
        'do nothing!
    End Sub
End Class
