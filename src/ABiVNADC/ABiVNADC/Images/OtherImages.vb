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
    Friend ReadOnly DungeonExit As New TextCanvas(New List(Of String) From
                                            {
                                                "+----------+",
                                                "|   EXIT   |",
                                                "+----------+"
                                            })
    <Extension>
    Function Image(featureType As FeatureType) As TextCanvas
        Select Case featureType
            Case FeatureType.DungeonExit
                Return DungeonExit
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

End Module
