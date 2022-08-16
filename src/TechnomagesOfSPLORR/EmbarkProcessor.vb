Module EmbarkProcessor
    Friend Sub Run(world As World)
        While world.CanContinue
            DescribeWorld(world)
            HandleCommand(world)
        End While
    End Sub

    Private Sub HandleCommand(world As World)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
        prompt.AddChoice(AbandonGameText)
        Select Case AnsiConsole.Prompt(prompt)
            Case AbandonGameText
                If ConfirmProcessor.Run("Are you sure you want to abandon the game?") Then
                    world.Team.Disband()
                End If
        End Select
    End Sub

    Private Sub DescribeWorld(world As World)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("Party:")
        For Each character In world.Team.Characters
            AnsiConsole.MarkupLine($"{character.Name}: {character.CharacterType.Name}")
        Next
    End Sub
End Module
