Module InPlayProcessor
    Friend Sub Run(world As World)
        While world.CanContinue
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            DescribeWorld(world, prompt)
            HandleCommand(world, prompt)
        End While
    End Sub

    Private Sub HandleCommand(world As World, prompt As SelectionPrompt(Of String))
        prompt.AddChoice(AbandonGameText)
        Select Case AnsiConsole.Prompt(prompt)
            Case TakeText
                TakeProcessor.Run(world)
            Case MoveText
                MoveProcessor.Run(world)
            Case AbandonGameText
                If ConfirmProcessor.Run("Are you sure you want to abandon the game?") Then
                    world.Team.Disband()
                End If
        End Select
    End Sub

    Private Sub DescribeWorld(world As World, prompt As SelectionPrompt(Of String))
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"Party: {world.Team.CharacterNames}")
        Dim location = world.Team.Location
        AnsiConsole.MarkupLine($"Location: {location.Name}")
        If location.HasRoutes Then
            prompt.AddChoice(MoveText)
            AnsiConsole.MarkupLine($"Exits: {location.RouteNames}")
        End If
        If location.HasItems Then
            prompt.AddChoice(TakeText)
            AnsiConsole.MarkupLine($"Items: {location.ItemStackNames}")
        End If
    End Sub
End Module
