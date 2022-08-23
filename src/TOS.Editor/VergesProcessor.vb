Module VergesProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Verge?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewText)
            Dim table = world.Verges.ToDictionary(Function(x) x.UniqueName, Function(x) x)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case NewText
                    RunNew(world)
                Case Else
                    RunEdit(world, table(answer))
            End Select
        Loop
    End Sub
    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Verge Type?[/]"}
        Dim table = world.VergeTypes.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        RunEdit(world, world.CreateVerge(newName, table(AnsiConsole.Prompt(prompt))))
    End Sub
    Private Sub RunEdit(world As World, verge As Verge)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Verge:")
            AnsiConsole.MarkupLine($"* Id: {verge.Id}")
            AnsiConsole.MarkupLine($"* Name: {verge.Name}")
            AnsiConsole.MarkupLine($"* Verge Type: {verge.VergeType.UniqueName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeVergeTypeText)
            If verge.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    RunChangeName(verge)
                Case ChangeVergeTypeText
                    RunChangeVergeType(world, verge)
                Case DeleteText
                    verge.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunChangeVergeType(world As World, verge As Verge)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Verge Type?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = world.VergeTypes.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                verge.VergeType = table(answer)
        End Select
    End Sub

    Private Sub RunChangeName(verge As Verge)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            verge.Name = newName
        End If
    End Sub
End Module
