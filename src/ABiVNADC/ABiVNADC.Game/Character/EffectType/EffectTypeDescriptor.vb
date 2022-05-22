Imports System.Text

Public Class EffectTypeDescriptor
    Property Name As String
    Property ApplyOn As Action(Of Character, StringBuilder)
    Property AttackDice As String
    Sub New()
        ApplyOn = Sub(c, b)

                  End Sub
        AttackDice = "0d1"
    End Sub
End Class
Module EffectTypeDescriptorExtensions
    Friend ReadOnly EffectTypeDescriptors As New Dictionary(Of EffectType, EffectTypeDescriptor) From
        {
            {
                EffectType.Infection,
                New EffectTypeDescriptor With
                {
                    .Name = "infection",
                    .ApplyOn = Sub(character, builder)
                                   Dim damage = RNG.RollDice("1d3/3")
                                   character.AddWounds(damage)
                                   builder.AppendLine($"{character.FullName} takes {damage} points of infection damage.")
                               End Sub
                }
            },
            {
                EffectType.Nausea,
                New EffectTypeDescriptor With
                {
                    .Name = "nausea",
                    .ApplyOn = Sub(character, builder)
                                   builder.AppendLine($"{character.FullName} feels queasy.")
                               End Sub,
                    .AttackDice = "-1d2/2"
                }
            }
        }
End Module