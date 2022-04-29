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
    Private ReadOnly DungeonExitSprite As New Sprite(DungeonExit, 22, 2)
    Friend ReadOnly ChestSprite As New Sprite(Chest, 22, 13)
    <Extension>
    Function Sprite(featureType As FeatureType) As Sprite
        Select Case FeatureType.DungeonExit
            Case FeatureType.DungeonExit
                Return DungeonExitSprite
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
