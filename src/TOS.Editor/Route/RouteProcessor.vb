Module RouteProcessor
    Friend Sub RunEdit(world As World, route As Route)
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
                Case ChangeFromLocationText
                    RunChangeFromLocation(world, route)
                Case ChangeNameText
                    RunChangeName(route)
                Case ChangeRouteTypeText
                    RunChangeRouteType(world, route)
                Case ChangeToLocationText
                    RunChangeToLocation(world, route)
                Case ChangeVergeText
                    RunChangeVerge(world, route)
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunChangeVerge(world As World, route As Route)
        Dim verge = PickThingie("Which Verge?", world.Verges, Function(x) x.UniqueName, True)
        If verge IsNot Nothing Then
            route.Verge = verge
        End If
    End Sub

    Private Sub RunChangeFromLocation(world As World, route As Route)
        Dim location = PickThingie("Which From Location?", world.Locations, Function(x) x.UniqueName, True)
        If location IsNot Nothing Then
            route.FromLocation = location
        End If
    End Sub

    Private Sub RunChangeToLocation(world As World, route As Route)
        Dim location = PickThingie("Which To Location?", world.Locations, Function(x) x.UniqueName, True)
        If location IsNot Nothing Then
            route.ToLocation = location
        End If
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
