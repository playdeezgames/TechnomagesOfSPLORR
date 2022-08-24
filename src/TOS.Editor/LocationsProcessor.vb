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
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If location.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    RunChangeName(location)
                Case DeleteText
                    location.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub
    Private Sub RunChangeName(location As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            location.Name = newName
        End If
    End Sub
End Module
