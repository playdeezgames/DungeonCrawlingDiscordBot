Module SellProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireTokens(
            tokens,
            "Sell what?",
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        RequireItemNameQuantity(
                            tokens,
                            AddressOf character.Inventory.FindItemsByName,
                            builder,
                            Sub(items)
                                Dim shoppe = location.Shoppe
                                Dim fullName = items.First.FullName
                                Dim quantity = items.LongCount
                                shoppe.SellItems(character, items, builder)
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
