Public Class ModifierTypeDescriptor
    Property NameDecorator As Func(Of String, String)
    Property Name As String
    Sub New()
        NameDecorator = Function(x) x
    End Sub
End Class
Module ModifierTypeDescriptorUtility
    Friend ReadOnly ModifierTypeDescriptors As New Dictionary(Of ModifierType, ModifierTypeDescriptor) From
        {
            {
                ModifierType.Attack,
                New ModifierTypeDescriptor With
                {
                    .Name = "valor",
                    .NameDecorator = Function(name)
                                         Return $"{name} of valor"
                                     End Function
                }
            },
            {
                ModifierType.Defend,
                New ModifierTypeDescriptor With
                {
                    .Name = "vinidication",
                    .NameDecorator = Function(name)
                                         Return $"{name} of vindication"
                                     End Function
                }
            },
            {
                ModifierType.Energy,
                New ModifierTypeDescriptor With
                {
                    .Name = "vigor",
                    .NameDecorator = Function(name)
                                         Return $"{name} of vigor"
                                     End Function
                }
            },
            {
                ModifierType.Health,
                New ModifierTypeDescriptor With
                {
                    .Name = "vitality",
                    .NameDecorator = Function(name)
                                         Return $"{name} of vitality"
                                     End Function
                }
            },
            {
                ModifierType.None,
                New ModifierTypeDescriptor
            }
        }
End Module
