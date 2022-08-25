Module CharacterProcessor
    Friend Sub Run(world As World, character As Character)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Character:")
            AnsiConsole.MarkupLine($"Id: {character.Id}")
            AnsiConsole.MarkupLine($"Name: {character.Name}")
            AnsiConsole.MarkupLine($"Type: {character.CharacterType.UniqueName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(GoBackText)
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub
End Module
