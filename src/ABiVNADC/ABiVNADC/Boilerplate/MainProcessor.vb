Public Module MainProcessor
    Private ProcessorTable As New Dictionary(Of String, Action(Of Player, StringBuilder, IEnumerable(Of String))) From
        {
            {AboutText, AddressOf AboutProcessor.Run},
            {AcceptText, AddressOf AcceptProcessor.Run},
            {AroundText, AddressOf AroundProcessor.Run},
            {BuyText, AddressOf BuyProcessor.Run},
            {BribeText, AddressOf BribeProcessor.Run},
            {CreditText, AddressOf CreditProcessor.Run},
            {DeliverText, AddressOf DeliverProcessor.Run},
            {DropText, AddressOf DropProcessor.Run},
            {EnemiesText, AddressOf EnemiesProcessor.Run},
            {EnterText, AddressOf EnterProcessor.Run},
            {EquipText, AddressOf EquipProcessor.Run},
            {ExitText, AddressOf ExitProcessor.Run},
            {FeaturesText, AddressOf FeaturesProcessor.Run},
            {FightText, AddressOf FightProcessor.Run},
            {GroundText, AddressOf GroundProcessor.Run},
            {HelpText, AddressOf HelpProcessor.Run},
            {InventoryText, AddressOf InventoryProcessor.Run},
            {LeftText, AddressOf LeftProcessor.Run},
            {LookText, AddressOf LookProcessor.Run},
            {MoveText, AddressOf MoveProcessor.Run},
            {OffersText, AddressOf OffersProcessor.Run},
            {PricesText, AddressOf PricesProcessor.Run},
            {QuestText, AddressOf QuestProcessor.Run},
            {RenameText, AddressOf RenameProcessor.Run},
            {RestText, AddressOf RestProcessor.Run},
            {RightText, AddressOf RightProcessor.Run},
            {RipText, AddressOf RipProcessor.Run},
            {RunText, AddressOf RunProcessor.Run},
            {SellText, AddressOf SellProcessor.Run},
            {StartText, AddressOf StartProcessor.Run},
            {StatusText, AddressOf StatusProcessor.Run},
            {TakeText, AddressOf TakeProcessor.Run},
            {UnequipText, AddressOf UnequipProcessor.Run},
            {UseText, AddressOf UseProcessor.Run}
        }

    Private Sub UnknownCommand(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        builder.AppendLine("Dunno what you mean. Mebbe you need to try `help`?")
    End Sub
    Function Run(player As Player, command As String) As String
        Dim tokens = command.Split(" "c)
        Dim processor As Action(Of Player, StringBuilder, IEnumerable(Of String)) = AddressOf UnknownCommand
        If Not ProcessorTable.TryGetValue(tokens.First.ToLower, processor) Then
            processor = AddressOf UnknownCommand
        End If
        Dim builder As New StringBuilder
        processor(player, builder, tokens.Skip(1))
        Return builder.ToString
    End Function
End Module
