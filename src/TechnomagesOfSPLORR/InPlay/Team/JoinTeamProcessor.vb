Module JoinTeamProcessor
    Friend Sub Run(world As World, character As Character)
        If Not character.CanJoin Then
            AnsiConsole.MarkupLine($"{character.FullName} will not join the team.")
            OkPrompt()
            Return
        End If
        character.Join()
        AnsiConsole.MarkupLine($"{character.FullName} joins the team.")
        OkPrompt()
    End Sub
End Module
