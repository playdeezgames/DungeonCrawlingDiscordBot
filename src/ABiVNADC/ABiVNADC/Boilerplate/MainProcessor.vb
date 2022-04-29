Public Module MainProcessor
    Private processorTable As New Dictionary(Of String, Func(Of Player, IEnumerable(Of String), String)) From
        {
            {AboutText, AddressOf AboutProcessor.Run},
            {AroundText, AddressOf AroundProcessor.Run},
            {CharactersText, AddressOf CharactersProcessor.Run},
            {CreateText, AddressOf CreateProcessor.Run},
            {DeleteText, AddressOf DeleteProcessor.Run},
            {DropText, AddressOf DropProcessor.Run},
            {DungeonsText, AddressOf DungeonsProcessor.Run},
            {EnemiesText, AddressOf EnemiesProcessor.Run},
            {EnterText, AddressOf EnterProcessor.Run},
            {EquipText, AddressOf EquipProcessor.Run},
            {FeaturesText, AddressOf FeaturesProcessor.Run},
            {FightText, AddressOf FightProcessor.Run},
            {GroundText, AddressOf GroundProcessor.Run},
            {HelpText, AddressOf HelpProcessor.Run},
            {InventoryText, AddressOf InventoryProcessor.Run},
            {LeftText, AddressOf LeftProcessor.Run},
            {LookText, AddressOf LookProcessor.Run},
            {MoveText, AddressOf MoveProcessor.Run},
            {RestText, AddressOf RestProcessor.Run},
            {RightText, AddressOf RightProcessor.Run},
            {RunText, AddressOf RunProcessor.Run},
            {StatusText, AddressOf StatusProcessor.Run},
            {SwitchText, AddressOf SwitchProcessor.Run},
            {TakeText, AddressOf TakeProcessor.Run},
            {UnequipText, AddressOf UnequipProcessor.Run},
            {UseText, AddressOf UseProcessor.Run}
        }

    Public Property ProcessorTable1 As Dictionary(Of String, Func(Of Player, IEnumerable(Of String), String))
        Get
            Return processorTable
        End Get
        Set(value As Dictionary(Of String, Func(Of Player, IEnumerable(Of String), String)))
            processorTable = value
        End Set
    End Property

    Private Function UnknownCommand(player As Player, tokens As IEnumerable(Of String)) As String
        Return "Dunno what you mean. Mebbe you need to try `help`?"
    End Function
    Function Run(player As Player, command As String) As String
        Dim tokens = command.Split(" "c)
        Dim processor As Func(Of Player, IEnumerable(Of String), String) = AddressOf UnknownCommand
        If Not ProcessorTable1.TryGetValue(tokens.First.ToLower, processor) Then
            processor = AddressOf UnknownCommand
        End If
        Return processor(player, tokens.Skip(1))
    End Function
End Module
