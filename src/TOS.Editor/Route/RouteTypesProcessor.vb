Module RouteTypesProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Route Type?",
            Function(x) x.RouteTypes,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunEdit(world As World, routeType As RouteType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Route Type:")
            AnsiConsole.MarkupLine($"* Id: {routeType.Id}")
            AnsiConsole.MarkupLine($"* Name: {routeType.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If routeType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    RunChangeName(routeType)
                Case DeleteText
                    routeType.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub
    Private Sub RunChangeName(routeType As RouteType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            routeType.Name = newName
        End If
    End Sub
    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            RunEdit(world, world.CreateRouteType(newName))
        End If
    End Sub
End Module
