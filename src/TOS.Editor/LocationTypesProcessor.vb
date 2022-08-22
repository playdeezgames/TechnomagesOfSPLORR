Module LocationTypesProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Location Type?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewText)
            Dim table = world.LocationTypes.ToDictionary(Function(x) x.UniqueName, Function(x) x)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case NewText
                    'RunNew(world)
                Case Else
                    RunEdit(world, table(answer))
            End Select
        Loop
    End Sub
    Private Sub RunEdit(world As World, locationType As LocationType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Location Type:")
            AnsiConsole.MarkupLine($"* Id: {locationType.Id}")
            AnsiConsole.MarkupLine($"* Name: {locationType.Name}")
            Dim statistics = locationType.Statistics
            If statistics.Any Then
                AnsiConsole.MarkupLine($"* Statistic Deltas:")
                For Each statistic In statistics
                    AnsiConsole.MarkupLine($"  * {statistic.Item1.Name}({statistic.Item1.DisplayName}): {statistic.Item2}")
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If locationType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            prompt.AddChoice(AddChangeStatisticText)
            If locationType.HasStatistics Then
                prompt.AddChoice(RemoveStatisticText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddChangeStatisticText
                    'RunAddChangeStatistic(world, locationType)
                Case ChangeNameText
                    'RunChangeName(locationType)
                Case DeleteText
                    'RunDelete(locationType)
                    Exit Do
                Case GoBackText
                    Exit Do
                Case RemoveStatisticText
                    'RunRemoveStatistic(world, locationType)
            End Select
        Loop
    End Sub

End Module
