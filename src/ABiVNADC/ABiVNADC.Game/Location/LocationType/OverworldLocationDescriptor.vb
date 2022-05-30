Friend Class OverworldLocationDescriptor
    Inherits LocationTypeDescriptor
    Sub New()
        MyBase.New(False, False)
    End Sub
    Public Overrides Sub HandleEnteredBy(character As Character, location As Location)
        location.Overworld.IncreasePeril()
    End Sub
End Class
