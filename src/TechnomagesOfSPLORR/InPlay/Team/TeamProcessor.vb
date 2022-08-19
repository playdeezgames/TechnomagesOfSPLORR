Module TeamProcessor
    Friend Sub Run(world As World)
        AnsiConsole.Clear()
        Dim character = ChooseCharacter("Which team member?", world.Team.Characters)
        If character Is Nothing Then
            Return
        End If
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"{character.FullName}:")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What would you like to do?[/]"}
        prompt.AddChoice(NeverMindText)
        If character.CanLeave Then
            prompt.AddChoice(RemoveText)
        End If
        Select Case AnsiConsole.Prompt(prompt)
            Case NeverMindText
                'do nothing
            Case RemoveText
                HandleRemove(character)
        End Select
    End Sub

    Private Sub HandleRemove(character As Character)
        If ConfirmProcessor.Run($"Are you sure you want {character.FullName} to leave the team?") Then
            AnsiConsole.MarkupLine($"{character.FullName} leaves the team.")
            character.Leave()
            OkPrompt()
        End If
    End Sub
End Module
