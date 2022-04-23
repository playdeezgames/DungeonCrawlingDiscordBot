Public Module MainProcessor
    Private processorTable As New Dictionary(Of String, Func(Of Player, IEnumerable(Of String), String)) From
        {
            {AroundText, AddressOf AroundProcessor.Run},
            {CharactersText, AddressOf CharactersProcessor.Run},
            {CreateText, AddressOf CreateProcessor.Run},
            {DeleteText, AddressOf DeleteProcessor.Run},
            {DropText, AddressOf DropProcessor.Run},
            {DungeonsText, AddressOf DungeonsProcessor.Run},
            {EnterText, AddressOf EnterProcessor.Run},
            {GroundText, AddressOf GroundProcessor.Run},
            {HelpText, AddressOf HelpProcessor.Run},
            {InventoryText, AddressOf InventoryProcessor.Run},
            {LeftText, AddressOf LeftProcessor.Run},
            {MoveText, AddressOf MoveProcessor.Run},
            {RightText, AddressOf RightProcessor.Run},
            {StatusText, AddressOf StatusProcessor.Run},
            {SwitchText, AddressOf SwitchProcessor.Run},
            {TakeText, AddressOf TakeProcessor.Run}
        }
    Private Function UnknownCommand(player As Player, tokens As IEnumerable(Of String)) As String
        Return "Dunno what you mean. Mebbe you need to try `help`?"
    End Function
    Function Run(player As Player, command As String) As String
        Dim tokens = command.Split(" "c)
        Dim processor As Func(Of Player, IEnumerable(Of String), String) = AddressOf UnknownCommand
        processorTable.TryGetValue(tokens.First.ToLower, processor)
        Return processor(player, tokens.Skip(1))
    End Function
End Module
