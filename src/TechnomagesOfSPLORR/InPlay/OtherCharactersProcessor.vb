Friend Module OtherCharactersProcessor
    Friend Sub Run(world As World)
        Dim location = world.Team.Location
        If Not location.HasOtherCharacters Then
            AnsiConsole.MarkupLine("There are no other characters here.")
            OkPrompt()
            Return
        End If
        AnsiConsole.Clear()
        Dim otherCharacter = ChooseCharacter("Which Character?", location.OtherCharacters)
        If otherCharacter Is Nothing Then
            Return
        End If
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"{otherCharacter.FullName}:")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "Now what?"}
        prompt.AddChoice(NeverMindText)
        If otherCharacter.CanJoin Then
            prompt.AddChoice(JoinTeamText)
        End If
        Select Case AnsiConsole.Prompt(prompt)
            Case JoinTeamText
                JoinTeamProcessor.Run(world, otherCharacter)
            Case NeverMindText
                'do nothing
        End Select
    End Sub
End Module
