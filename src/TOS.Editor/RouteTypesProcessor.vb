Module RouteTypesProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Route Type?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewText)
            Dim table = world.RouteTypes.ToDictionary(Function(x) x.UniqueName, Function(x) x)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case NewText
                    RunNew(world)
                Case Else
                    RunEdit(world, table(answer))
            End Select
        Loop
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
