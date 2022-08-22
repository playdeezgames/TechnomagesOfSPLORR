Module StatisticTypesProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Statistic Type?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewText)
            Dim table = world.StatisticTypes.ToDictionary(Function(x) x.UniqueName, Function(x) x)
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
                    'RunChangeDisplayName(CharacterType)
                Case ChangeNameText
                    'RunChangeName(CharacterType)
                Case DeleteText
                    'RunDelete(CharacterType)
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

End Module
