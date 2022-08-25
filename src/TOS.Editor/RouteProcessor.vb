Module RouteProcessor
    Friend Sub Run(world As World, route As Route)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Route:")
            AnsiConsole.MarkupLine($"Id: {route.Id}")
            AnsiConsole.MarkupLine($"Name: {route.Name}")
            AnsiConsole.MarkupLine($"Type: {route.RouteType.UniqueName}")
            AnsiConsole.MarkupLine($"From Location: {route.FromLocation.UniqueName}")
            AnsiConsole.MarkupLine($"Verge: {route.Verge.UniqueName}")
            AnsiConsole.MarkupLine($"To Location: {route.ToLocation.UniqueName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(
                GoBackText,
                ChangeNameText,
                ChangeRouteTypeText,
                ChangeFromLocationText,
                ChangeVergeText,
                ChangeToLocationText)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    RunChangeName(route)
                Case ChangeRouteTypeText
                    RunChangeRouteType(world, route)
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunChangeRouteType(world As World, route As Route)
        Dim routeType = PickThingie("Which route type?", world.RouteTypes, Function(x) x.UniqueName, True)
        If routeType IsNot Nothing Then
            route.RouteType = routeType
        End If
    End Sub

    Private Sub RunChangeName(route As Route)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            route.Name = newName
        End If
    End Sub
End Module
