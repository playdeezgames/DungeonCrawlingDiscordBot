Imports System.Text

Public Class EffectTypeDescriptor
    Property Name As String
    Property ApplyOn As Action(Of Character, StringBuilder)
End Class
Module EffectTypeDescriptorExtensions
    Friend ReadOnly EffectTypeDescriptors As New Dictionary(Of EffectType, EffectTypeDescriptor) From
        {
            {
                EffectType.Nausea,
                New EffectTypeDescriptor With
                {
                    .Name = "nausea",
                    .ApplyOn = Sub(character, builder)
                                   builder.AppendLine($"{character.FullName} feels queasy.")
                               End Sub
                }
            }
        }
End Module