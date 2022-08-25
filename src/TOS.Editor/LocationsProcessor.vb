Module LocationsProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Location?",
            Function(x) x.Locations,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunNew(world As World)
        'Throw New NotImplementedException
    End Sub
    Private Sub RunEdit(world As World, location As Location)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Location:")
            AnsiConsole.MarkupLine($"* Id: {location.Id}")
            AnsiConsole.MarkupLine($"* Name: {location.Name}")
            If location.HasExits Then
                AnsiConsole.MarkupLine("* Exits:")
                For Each route In location.Exits
                    AnsiConsole.MarkupLine($"  * {route.UniqueName} -> {route.Verge.UniqueName} -> {route.ToLocation.UniqueName}")
                Next
            End If
            If location.HasEntrances Then
                AnsiConsole.MarkupLine("* Entrances:")
                For Each route In location.Entrances
                    AnsiConsole.MarkupLine($"  * {route.UniqueName} <- {route.Verge.UniqueName} <- {route.FromLocation.UniqueName}")
                Next
            End If
            If location.HasCharacters Then
                AnsiConsole.MarkupLine("* Characters:")
                For Each character In location.Characters
                    AnsiConsole.MarkupLine($"  * {character.UniqueName}")
                Next
            End If
            If location.HasItems Then
                AnsiConsole.MarkupLine($"* Items:")
                For Each item In location.Items
                    AnsiConsole.MarkupLine($"  * {item.UniqueName}")
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If location.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            prompt.AddChoice(AddItemText)
            If location.HasItems Then
                prompt.AddChoice(RemoveItemText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddItemText
                    RunAddItem(world, location)
                Case ChangeNameText
                    RunChangeName(location)
                Case DeleteText
                    location.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
                Case RemoveItemText
                    RunRemoveItem(location)
            End Select
        Loop
    End Sub

    Private Sub RunAddItem(world As World, location As Location)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What Item Type?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = world.ItemTypes.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                location.Inventory.Add(world.CreateItem(table(answer)))
        End Select
    End Sub

    Private Sub RunRemoveItem(location As Location)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Item?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = location.Items.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                table(answer).Destroy()
        End Select
    End Sub

    Private Sub RunChangeName(location As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            location.Name = newName
        End If
    End Sub
End Module
