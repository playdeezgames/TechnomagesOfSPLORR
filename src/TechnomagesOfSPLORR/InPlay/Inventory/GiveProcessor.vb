Module GiveProcessor
    Friend Sub Run(itemStack As IEnumerable(Of Item), teammates As IEnumerable(Of Character), giver As Character)
        Dim items = itemStack.Take(ChooseQuantity("Give how many?", itemStack.Count))
        If Not items.Any Then
            Return
        End If
        Dim teammate = ChooseCharacter("Give to whom?", teammates)
        If teammate Is Nothing Then
            Return
        End If
        If items.Any Then
            teammate.Inventory.Add(items)
            AnsiConsole.MarkupLine($"{giver.FullName} gives {items.Count} {items.First.Name} to {teammate.FullName}.")
            OkPrompt()
        End If
    End Sub


End Module
