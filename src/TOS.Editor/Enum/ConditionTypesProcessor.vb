Module ConditionTypesProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Condition Type?",
            Function(x) x.ConditionTypes,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
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
        RunEdit(world.CreateConditionType(newName, newDisplayName))
    End Sub
    Private Sub RunEdit(conditionType As ConditionType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Equip Slot:")
            AnsiConsole.MarkupLine($"* Id: {conditionType.Id}")
            AnsiConsole.MarkupLine($"* Name: {conditionType.Name}")
            AnsiConsole.MarkupLine($"* Display Name: {conditionType.DisplayName}")
            If conditionType.HasStatisticRange Then
                AnsiConsole.MarkupLine($"* Statistic Range(s):")
                For Each entry In conditionType.StatisticRanges
                    AnsiConsole.MarkupLine($"  * {entry.Item1.UniqueName}: {entry.Item2.Item1} thru {entry.Item2.Item2}")
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeDisplayNameText)
            prompt.AddChoice(AddChangeStatisticRangeText)
            If conditionType.HasStatisticRange Then
                prompt.AddChoice(RemoveStatisticRangeText)
            End If
            If conditionType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddChangeStatisticRangeText
                    RunAddChangeStatisticRange(conditionType)
                Case ChangeDisplayNameText
                    RunChangeDisplayName(conditionType)
                Case ChangeNameText
                    RunChangeName(conditionType)
                Case DeleteText
                    conditionType.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
                Case RemoveStatisticRangeText
                    RunRemoveStatisticRange(conditionType)
            End Select
        Loop
    End Sub

    Private Sub RunRemoveStatisticRange(conditionType As ConditionType)
        Dim statisticType = PickThingie("Which Statistic Type?", conditionType.StatisticRanges.Select(Function(x) x.Item1), Function(x) x.UniqueName, True)
        If statisticType IsNot Nothing Then
            conditionType.StatisticRange(statisticType) = Nothing
        End If
    End Sub

    Private Sub RunAddChangeStatisticRange(conditionType As ConditionType)
        Dim statisticType = PickThingie("Which Statistic Type?", conditionType.World.StatisticTypes, Function(x) x.UniqueName, True)
        If statisticType Is Nothing Then
            Return
        End If
        Dim minimum = AnsiConsole.Ask("[olive]Minimum Value?[/]", Long.MinValue)
        Dim maximum = AnsiConsole.Ask("[olive]Maximum Value?[/]", Long.MaxValue)
        conditionType.StatisticRange(statisticType) = (minimum, maximum)
    End Sub

    Private Sub RunChangeDisplayName(conditionType As ConditionType)
        Dim newName = AnsiConsole.Ask("[olive]New Display Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            conditionType.DisplayName = newName
        End If
    End Sub

    Private Sub RunChangeName(conditionType As ConditionType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            conditionType.Name = newName
        End If
    End Sub
End Module
