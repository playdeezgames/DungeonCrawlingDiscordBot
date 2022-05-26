Imports System.Text

Friend Class EscapeScrollDescriptor
    Inherits ItemTypeDescriptor
    Public Overrides Sub OnUse(character As Character, item As Item, builder As StringBuilder)
        builder.AppendLine($"{character.FullName} uses the escape scroll!")
        item.Destroy()
        Dim direction = RNG.FromEnumerable(character.Location.Routes.Keys)
        character.Location = character.Location.Routes(direction).ToLocation
        If character.Location.HasEnemies(character) Then
            builder.AppendLine($"There are enemies here that immediately attack {character.FullName}!")
        End If
    End Sub
    Sub New()
        MyBase.New("escape scroll", True)
        CanBuyGenerator = MakeBooleanGenerator(3, 1)
        BuyPriceDice = "25d1+2d25"
        InventoryEncumbrance = 0
        Aliases = New List(Of String) From {"scroll"}
    End Sub
End Class
