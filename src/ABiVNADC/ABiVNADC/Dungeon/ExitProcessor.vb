﻿Module ExitProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is just `exit`."
        End If
        Return RequireCharacter(
            player,
            Function(character)
                Return RequireLocation(
                    character,
                    Function(location)
                        If character.InCombat Then
                            Return $"{character.FullName} cannot exit while in combat!"
                        End If
                        If Not location.Features.Any(Function(x) x.FeatureType = FeatureType.DungeonExit) Then
                            Return "There is no exit here!"
                        End If
                        character.Location = Nothing
                        Return $"{character.FullName} leaves the dungeon."
                    End Function)
            End Function)
    End Function
End Module
