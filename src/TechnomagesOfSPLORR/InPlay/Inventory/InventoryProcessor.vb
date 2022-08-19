Module InventoryProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Inventory:")
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
                If character.CanGive Then
                    prompt.AddChoice(GiveText)
                End If
                If character.CanEquip(itemStack.First) Then
                    prompt.AddChoice(EquipText)
                End If
                prompt.AddChoice(DropText)
                Select Case AnsiConsole.Prompt(prompt)
                    Case NeverMindText
                        'do nothing
                    Case DropText
                        HandleDrop(itemStack, character.Location)
                        Exit Do
                    Case GiveText
                        HandleGive(itemStack, character.Teammates, character)
                End Select
            Loop
        Loop
    End Sub

    Private Sub HandleGive(itemStack As IEnumerable(Of Item), teammates As IEnumerable(Of Character), giver As Character)
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

    Private Sub HandleDrop(itemStack As IEnumerable(Of Item), location As Location)
        location.Inventory.Add(itemStack.Take(ChooseQuantity("Drop how many?", itemStack.Count)))
    End Sub
End Module
