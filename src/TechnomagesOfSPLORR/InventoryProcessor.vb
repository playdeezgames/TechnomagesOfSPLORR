Module InventoryProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim character = ChooseCharacter("Which team member?", world.Team.Characters)
            If character Is Nothing Then
                Exit Do
            End If
            Do
                Dim itemStacks = character.Inventory.ItemStacks
                If Not itemStacks.Any Then
                    AnsiConsole.MarkupLine($"{character.FullName} has no items.")
                    OkPrompt()
                    Exit Do
                End If
                Dim itemStack = ChooseItemStack("Select item:", character.Inventory.ItemStacks)
                If Not itemStack.Any Then
                    Exit Do
                End If
                Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What do you want to do?[/]"}
                prompt.AddChoice(NeverMindText)
                prompt.AddChoice(DropText)
                Select Case AnsiConsole.Prompt(prompt)
                    Case NeverMindText
                        'do nothing
                    Case DropText
                        HandleDrop(itemStack, character.Location)
                        Exit Do
                End Select
            Loop
        Loop
    End Sub

    Private Sub HandleDrop(itemStack As IEnumerable(Of Item), location As Location)
        location.Inventory.Add(itemStack.Take(ChooseQuantity("Drop how many?", itemStack.Count)))
    End Sub
End Module
