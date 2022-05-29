Friend Class StartingPotionIncentiveDescriptor
    Inherits IncentiveTypeDescriptor
    Sub New()
        MyBase.New("starting potion", 10)
    End Sub
    Public Overrides Sub ApplyTo(character As Character, level As Long)
        While level > 0
            level -= 1
            If RNG.RollDice("1d2/2") > 0 Then
                character.Inventory.Add(Item.Create(ItemType.Potion))
            End If
        End While
    End Sub
End Class
