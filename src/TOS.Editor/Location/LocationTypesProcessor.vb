Module LocationTypesProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Location Type?",
            Function(x) x.LocationTypes,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunEdit(locationType As LocationType)
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
                    RunAddChangeStatistic(locationType)
                Case ChangeNameText
                    RunChangeName(locationType)
                Case DeleteText
                    locationType.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
                Case RemoveStatisticText
                    RunRemoveStatistic(locationType)
            End Select
        Loop
    End Sub
    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            RunEdit(world.CreateLocationType(newName))
        End If
    End Sub

    Private Sub RunChangeName(locationType As LocationType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            locationType.Name = newName
        End If
    End Sub
    Private Sub RunAddChangeStatistic(locationType As LocationType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Add/Change which statistic?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = locationType.World.StatisticTypes.ToDictionary(Of String, StatisticType)(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                Dim statisticValue = AnsiConsole.Ask(Of Long)("[olive]Statistic Value: [/]")
                locationType.Statistic(table(answer)) = statisticValue
        End Select
    End Sub
    Private Sub RunRemoveStatistic(locationType As LocationType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Remove which statistic?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = locationType.World.StatisticTypes.ToDictionary(Of String, StatisticType)(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                locationType.Statistic(table(answer)) = Nothing
        End Select
    End Sub

End Module
