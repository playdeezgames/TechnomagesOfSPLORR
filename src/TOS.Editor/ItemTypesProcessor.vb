Module ItemTypesProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Item Type?",
            Function(x) x.ItemTypes,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            RunEdit(world, world.CreateItemType(newName))
        End If
    End Sub
    Private Sub RunEdit(world As World, itemType As ItemType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Item Type:")
            AnsiConsole.MarkupLine($"* Id: {itemType.Id}")
            AnsiConsole.MarkupLine($"* Name: {itemType.Name}")

            If itemType.HasStatistics Then
                AnsiConsole.MarkupLine($"* Statistics:")
                For Each entry In itemType.Statistics
                    AnsiConsole.MarkupLine($"  * {entry.Item1.UniqueName}: {entry.Item2}")
                Next
            End If

            AnsiConsole.MarkupLine($"* Equip Slots:")

            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeDisplayNameText)
            If itemType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    RunChangeName(itemType)
                Case DeleteText
                    RunDelete(itemType)
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub
    Private Sub RunChangeName(itemType As ItemType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            itemType.Name = newName
        End If
    End Sub

    Private Sub RunDelete(itemType As ItemType)
        itemType.Destroy()
    End Sub
End Module
