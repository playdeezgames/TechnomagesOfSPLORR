Module CharacterTypeProcessor
    Friend Sub Run(characterType As CharacterType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Character Type:")
            AnsiConsole.MarkupLine($"Id: {characterType.Id}")
            AnsiConsole.MarkupLine($"Name: {characterType.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    RunChangeName(characterType)
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunChangeName(characterType As CharacterType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            characterType.Name = newName
        End If
    End Sub
End Module
