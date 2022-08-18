Module TeamProcessor
    Friend Sub Run(world As World)
        AnsiConsole.Clear()
        Dim character = ChooseCharacter("Which team member?", world.Team.Characters)
        If character Is Nothing Then
            Return
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What would you like to do?[/]"}
        prompt.AddChoice(NeverMindText)
        prompt.AddChoice(RemoveText)
        Select Case AnsiConsole.Prompt(prompt)
            Case NeverMindText
                'do nothing
            Case RemoveText
                HandleRemove(character)
        End Select
    End Sub

    Private Sub HandleRemove(character As Character)
        AnsiConsole.MarkupLine($"{character.FullName} leaves the team.")
        character.Leave()
    End Sub
End Module
