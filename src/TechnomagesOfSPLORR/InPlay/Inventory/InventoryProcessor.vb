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
                        DropProcessor.Run(itemStack, character.Location)
                        Exit Do
                    Case GiveText
                        GiveProcessor.Run(itemStack, character.Teammates, character)
                End Select
            Loop
        Loop
    End Sub
End Module
