Module RemoveFromTeamProcessor
    Friend Sub Run(character As Character)
        If ConfirmProcessor.Run($"Are you sure you want {character.FullName} to leave the team?") Then
            AnsiConsole.MarkupLine($"{character.FullName} leaves the team.")
            character.Leave()
            OkPrompt()
        End If
    End Sub
End Module
