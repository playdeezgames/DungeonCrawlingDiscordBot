﻿Friend Class MacguffinDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("macguffin")
        QuestTargetWeight = 1
        QuestTargetQuantityDice = "1d1"
        SpawnCount = AddressOf RareSpawn
        IsTrophy = True
    End Sub

End Class
