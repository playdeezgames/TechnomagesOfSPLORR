Module ItemProcessor
    Friend Sub RunEdit(item As Item)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Item:")
            AnsiConsole.MarkupLine($"* Id: {item.Id}")
            AnsiConsole.MarkupLine($"* Type: {item.ItemType.UniqueName}")
            If item.IsEquipped Then
                AnsiConsole.MarkupLine("* Equipped On:")
                For Each entry In item.EquippedOn
                    AnsiConsole.MarkupLine($"  * {entry.Item1.UniqueName}: {entry.item2.UniqueName}")
                Next
            End If
            If item.HasLocation Then
                AnsiConsole.MarkupLine($"* Location: {item.Location.UniqueName}")
            End If
            If item.HasCharacter Then
                AnsiConsole.MarkupLine($"* Character: {item.Character.UniqueName}")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(NeverMindText)
            If item.IsEquipped Then
                prompt.AddChoice(UnequipItemText)
            End If
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case NeverMindText
                    Exit Do
                Case UnequipItemText
                    RunUnequipItem(item)
            End Select
        Loop
    End Sub

    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Item?",
            Function(x) x.Items,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub

    Private Sub RunUnequipItem(item As Item)
        item.Unequip()
    End Sub

    Private Sub RunNew(world As World)
        Dim itemType = PickThingie("Which Item Type?", world.ItemTypes, Function(x) x.UniqueName, True)
        If itemType IsNot Nothing Then
            RunEdit(world.CreateItem(itemType))
        End If
    End Sub
End Module
