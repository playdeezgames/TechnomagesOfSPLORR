Module VergeTypesProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Verge Type?",
            Function(x) x.VergeTypes,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunEdit(world As World, vergeType As VergeType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Verge Type:")
            AnsiConsole.MarkupLine($"* Id: {vergeType.Id}")
            AnsiConsole.MarkupLine($"* Name: {vergeType.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If vergeType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    RunChangeName(vergeType)
                Case DeleteText
                    vergeType.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub
    Private Sub RunChangeName(vergeType As VergeType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            vergeType.Name = newName
        End If
    End Sub
    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            RunEdit(world, world.CreateVergeType(newName))
        End If
    End Sub
End Module
