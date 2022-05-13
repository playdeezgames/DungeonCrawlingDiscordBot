Imports System.Runtime.CompilerServices

Module OtherImages
    Friend ReadOnly Chest As New TextCanvas(New List(Of String) From
                                            {
                                                "............",
                                                "............",
                                                "............",
                                                "..@|@@@@@|@.",
                                                ".@@|@@@@@|@@",
                                                ".@@|@@@@@|@@",
                                                ".@@@@@@@@@@@",
                                                ".-----------",
                                                ".@@@@@@@@@@@",
                                                ".@@@@   @@@@",
                                                ".@@@@@ @@@@@",
                                                ".@@@@@@@@@@@"
                                            })
    Private ReadOnly DungeonExit As New TextCanvas(New List(Of String) From
                                            {
                                                "+----------+",
                                                "|   EXIT   |",
                                                "+----------+"
                                            })
    Private ReadOnly VomitPuddle As New TextCanvas(New List(Of String) From
                                            {
                                                " ___________ ",
                                                "(           )",
                                                " ) *VOMIT* ( ",
                                                "(___________)"
                                            })
    Private ReadOnly DungeonExitSprite As New Sprite(DungeonExit, 22, 2)
    Private ReadOnly VomitPuddleSprite As New Sprite(VomitPuddle, 22, 23)
    Friend ReadOnly ChestSprite As New Sprite(Chest, 22, 13)
    <Extension>
    Function Sprite(featureType As FeatureType) As Sprite
        Select Case featureType
            Case FeatureType.DungeonExit
                Return DungeonExitSprite
            Case FeatureType.VomitPuddle
                Return VomitPuddleSprite
            Case Else
                Return Nothing
        End Select
    End Function
End Module
