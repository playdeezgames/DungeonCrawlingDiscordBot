Public Module MainProcessor
    Private ReadOnly CommandAliases As New Dictionary(Of String, String) From
        {
            {"a", AroundText},
            {"attack", FightText},
            {"drink", UseText},
            {"eat", UseText},
            {"enc", EncumbranceText},
            {"doff", UnequipText},
            {"don", EquipText},
            {"f", FightText},
            {"g", GroundText},
            {"i", InventoryText},
            {"in", EnterText},
            {"hit", FightText},
            {"l", LeftText},
            {"m", MoveText},
            {"out", ExitText},
            {"quaff", UseText},
            {"r", RightText},
            {"stat", StatusText},
            {"stats", StatusText},
            {"wear", EquipText},
            {"wield", EquipText},
            {"z", RestText},
            {"zz", RestText},
            {"zzz", RestText}
        }
    Private ReadOnly ProcessorTable As New Dictionary(Of String, Action(Of Player, StringBuilder, IEnumerable(Of String))) From
        {
            {AboutText, AddressOf AboutProcessor.Run},
            {AcceptText, AddressOf AcceptProcessor.Run},
            {AroundText, AddressOf AroundProcessor.Run},
            {BuyText, AddressOf BuyProcessor.Run},
            {BribeText, AddressOf BribeProcessor.Run},
            {CraftText, AddressOf CraftProcessor.Run},
            {CreditText, AddressOf CreditProcessor.Run},
            {DeliverText, AddressOf DeliverProcessor.Run},
            {DropText, AddressOf DropProcessor.Run},
            {EncumbranceText, AddressOf EncumbranceProcessor.Run},
            {EnemiesText, AddressOf EnemiesProcessor.Run},
            {EnterText, AddressOf EnterProcessor.Run},
            {EquipText, AddressOf EquipProcessor.Run},
            {EquipmentText, AddressOf EquipmentProcessor.Run},
            {ExitText, AddressOf ExitProcessor.Run},
            {FeaturesText, AddressOf FeaturesProcessor.Run},
            {FightText, AddressOf FightProcessor.Run},
            {ForageText, AddressOf ForageProcessor.Run},
            {GroundText, AddressOf GroundProcessor.Run},
            {HelpText, AddressOf HelpProcessor.Run},
            {IncentivesText, AddressOf IncentivesProcessor.Run},
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
        Dim firstToken = tokens.First.ToLower
        If CommandAliases.ContainsKey(firstToken) Then
            firstToken = CommandAliases(firstToken)
        End If
        If Not ProcessorTable.TryGetValue(firstToken, processor) Then
            processor = AddressOf UnknownCommand
        End If
        Dim builder As New StringBuilder
        processor(player, builder, tokens.Skip(1))
        Return builder.ToString
    End Function
End Module
