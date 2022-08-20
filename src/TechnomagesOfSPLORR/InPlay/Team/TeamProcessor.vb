Module TeamProcessor
    Friend Sub Run(world As World)
        AnsiConsole.Clear()
        Dim character = ChooseCharacter("Which team member?", world.Team.Characters)
        If character Is Nothing Then
            Return
        End If
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"{character.FullName}:")
        Dim statistics = character.Statistics
        For Each statistic In statistics
            AnsiConsole.MarkupLine($"{statistic.DisplayName}: ")
        Next
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What would you like to do?[/]"}
        prompt.AddChoice(NeverMindText)
        If character.CanLeave Then
            prompt.AddChoice(RemoveText)
        End If
        Select Case AnsiConsole.Prompt(prompt)
            Case NeverMindText
                'do nothing
            Case RemoveText
                RemoveFromTeamProcessor.Run(character)
        End Select
    End Sub
End Module
