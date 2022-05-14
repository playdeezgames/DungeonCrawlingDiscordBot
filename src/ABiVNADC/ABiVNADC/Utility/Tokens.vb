﻿Module Tokens
    Friend Const AboutText = "about"
    Friend Const AllText = "all"
    Friend Const AroundText = "around"
    Friend Const BribeText = "bribe"
    Friend Const BuyText = "buy"
    Friend Const CreditText = "credit"
    Friend Const DropText = "drop"
    Friend Const EnemiesText = "enemies"
    Friend Const EnterText = "enter"
    Friend Const EquipText = "equip"
    Friend Const ExitText = "exit"
    Friend Const FeaturesText = "features"
    Friend Const FightText = "fight"
    Friend Const GroundText = "ground"
    Friend Const HelpText = "help"
    Friend Const InventoryText = "inventory"
    Friend Const LeftText = "left"
    Friend Const LookText = "look"
    Friend Const MoveText = "move"
    Friend Const OffersText = "offers"
    Friend Const PricesText = "prices"
    Friend Const QuestText = "quest"
    Friend Const RenameText = "rename"
    Friend Const RestText = "rest"
    Friend Const RightText = "right"
    Friend Const RipText = "rip"
    Friend Const RunText = "run"
    Friend Const SellText = "sell"
    Friend Const StartText = "start"
    Friend Const StatusText = "status"
    Friend Const TakeText = "take"
    Friend Const UnequipText = "unequip"
    Friend Const UseText = "use"
    Function StitchTokens(tokens As IEnumerable(Of String)) As String
        Return String.Join(" "c, tokens)
    End Function
End Module
