Module StatisticTypesProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Statistic Type?",
            Function(x) x.StatisticTypes,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub

    Private Sub RunEdit(world As World, statisticType As StatisticType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Statistic Type:")
            AnsiConsole.MarkupLine($"* Id: {statisticType.Id}")
            AnsiConsole.MarkupLine($"* Name: {statisticType.Name}")
            AnsiConsole.MarkupLine($"* Display Name: {statisticType.DisplayName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeDisplayNameText)
            If statisticType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeDisplayNameText
                    RunChangeDisplayName(statisticType)
                Case ChangeNameText
                    RunChangeName(statisticType)
                Case DeleteText
                    RunDelete(statisticType)
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunChangeDisplayName(statisticType As StatisticType)
        Dim newName = AnsiConsole.Ask("[olive]New Display Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            statisticType.DisplayName = newName
        End If
    End Sub

    Private Sub RunChangeName(statisticType As StatisticType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            statisticType.Name = newName
        End If
    End Sub

    Private Sub RunDelete(statisticType As StatisticType)
        statisticType.Destroy()
    End Sub

    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim newDisplayName = AnsiConsole.Ask("[olive]New Display Name:[/]", "")
        If String.IsNullOrWhiteSpace(newDisplayName) Then
            Return
        End If
        RunEdit(world, world.CreateStatisticType(newName, newDisplayName))
    End Sub
End Module
