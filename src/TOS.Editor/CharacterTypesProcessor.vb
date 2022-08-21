Module CharacterTypesProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Character Type:[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewText)
            Dim table = world.CharacterTypes.ToDictionary(Function(x) x.UniqueName, Function(x) x)
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
    Private Sub RunEdit(world As World, characterType As CharacterType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Character Type:")
            AnsiConsole.MarkupLine($"* Id: {characterType.Id}")
            AnsiConsole.MarkupLine($"* Name: {characterType.Name}")
            Dim statistics = characterType.Statistics
            If statistics.Any Then
                AnsiConsole.MarkupLine($"* Statistic Deltas:")
                For Each statistic In statistics
                    AnsiConsole.MarkupLine($"  * {statistic.Item1.Name}({statistic.Item1.DisplayName}): {statistic.Item2}")
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If characterType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            prompt.AddChoice(AddChangeStatisticText)
            If characterType.HasStatistics Then
                prompt.AddChoice(RemoveStatisticText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddChangeStatisticText
                    RunAddChangeStatistic(world, characterType)
                Case ChangeNameText
                    RunChangeName(characterType)
                Case DeleteText
                    RunDelete(characterType)
                    Exit Do
                Case GoBackText
                    Exit Do
                Case RemoveStatisticText
                    RunRemoveStatistic(world, characterType)
            End Select
        Loop
    End Sub

    Private Sub RunRemoveStatistic(world As World, characterType As CharacterType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Remove which statistic?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = world.StatisticTypes.ToDictionary(Of String, StatisticType)(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                characterType.Statistic(table(answer)) = Nothing
        End Select
    End Sub

    Private Sub RunAddChangeStatistic(world As World, characterType As CharacterType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Add/Change which statistic?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = world.StatisticTypes.ToDictionary(Of String, StatisticType)(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                Dim statisticValue = AnsiConsole.Ask(Of Long)("[olive]Statistic Value: [/]")
                characterType.Statistic(table(answer)) = statisticValue
        End Select
    End Sub

    Private Sub RunDelete(characterType As CharacterType)
        characterType.Destroy()
    End Sub

    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            RunEdit(world, world.CreateCharacterType(newName))
        End If
    End Sub

    Private Sub RunChangeName(characterType As CharacterType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            characterType.Name = newName
        End If
    End Sub
End Module
