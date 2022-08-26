Module LocationsProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Location?",
            Function(x) x.Locations,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim locationType = PickThingie(Of LocationType)("Location Type:", world.LocationTypes, Function(x) x.UniqueName, False)
        RunEdit(world, world.CreateLocation(newName, locationType))
    End Sub
    Private Sub RunEdit(world As World, location As Location)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Location:")
            AnsiConsole.MarkupLine($"* Id: {location.Id}")
            AnsiConsole.MarkupLine($"* Name: {location.Name}")
            If location.HasExits Then
                AnsiConsole.MarkupLine("* Exits:")
                For Each route In location.Exits
                    AnsiConsole.MarkupLine($"  * {route.UniqueName} -> {route.Verge.UniqueName} -> {route.ToLocation.UniqueName}")
                Next
            End If
            If location.HasEntrances Then
                AnsiConsole.MarkupLine("* Entrances:")
                For Each route In location.Entrances
                    AnsiConsole.MarkupLine($"  * {route.UniqueName} <- {route.Verge.UniqueName} <- {route.FromLocation.UniqueName}")
                Next
            End If
            If location.HasCharacters Then
                AnsiConsole.MarkupLine("* Characters:")
                For Each character In location.Characters
                    AnsiConsole.MarkupLine($"  * {character.UniqueName}")
                Next
            End If
            If location.HasItems Then
                AnsiConsole.MarkupLine($"* Items:")
                For Each item In location.Items
                    AnsiConsole.MarkupLine($"  * {item.UniqueName}")
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If location.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            prompt.AddChoice(AddExitText)
            If location.HasExits Then
                prompt.AddChoice(EditExitText)
                prompt.AddChoice(RemoveExitText)
            End If
            prompt.AddChoice(AddEntranceText)
            If location.HasEntrances Then
                prompt.AddChoice(EditEntranceText)
                prompt.AddChoice(RemoveEntranceText)
            End If
            If location.HasCharacters Then
                prompt.AddChoice(EditCharacterText)
            End If
            prompt.AddChoice(AddItemText)
            If location.HasItems Then
                prompt.AddChoice(RemoveItemText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddEntranceText
                    RunAddEntrance(world, location)
                Case AddExitText
                    RunAddExit(world, location)
                Case AddItemText
                    RunAddItem(world, location)
                Case ChangeNameText
                    RunChangeName(location)
                Case DeleteText
                    location.Destroy()
                    Exit Do
                Case EditCharacterText
                    RunEditCharacter(world, location)
                Case EditEntranceText
                    RunEditEntrance(world, location)
                Case EditExitText
                    RunEditExit(world, location)
                Case GoBackText
                    Exit Do
                Case RemoveEntranceText
                    RunRemoveEntrance(location)
                Case RemoveExitText
                    RunRemoveExit(location)
                Case RemoveItemText
                    RunRemoveItem(location)
            End Select
        Loop
    End Sub

    Private Sub RunEditCharacter(world As World, location As Location)
        Dim character = PickThingie("Which Character?", location.Characters, Function(x) x.UniqueName, True)
        If character IsNot Nothing Then
            CharacterProcessor.RunEdit(world, character)
        End If
    End Sub

    Private Sub RunEditExit(world As World, location As Location)
        Dim route = PickThingie("Which Exit?", location.Exits, Function(x) x.UniqueName, True)
        If route IsNot Nothing Then
            RouteProcessor.RunEdit(world, route)
        End If
    End Sub

    Private Sub RunEditEntrance(world As World, location As Location)
        Dim route = PickThingie("Which Exit?", location.Entrances, Function(x) x.UniqueName, True)
        If route IsNot Nothing Then
            RouteProcessor.RunEdit(world, route)
        End If
    End Sub

    Private Sub RunRemoveExit(location As Location)
        Dim route = PickThingie("Which Exit?", location.Exits, Function(x) x.UniqueName, True)
        If route IsNot Nothing Then
            route.Destroy()
        End If
    End Sub

    Private Sub RunRemoveEntrance(location As Location)
        Dim route = PickThingie("Which Entrace?", location.Entrances, Function(x) x.UniqueName, True)
        If route IsNot Nothing Then
            route.Destroy()
        End If
    End Sub

    Private Sub RunAddExit(world As World, fromLocation As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim routeType = PickThingie(Of RouteType)("Route Type:", world.RouteTypes, Function(x) x.UniqueName, False)
        Dim verge = PickThingie(Of Verge)("Verge:", world.Verges, Function(x) x.UniqueName, False)
        Dim toLocation = PickThingie(Of Location)("To Location:", world.Locations, Function(x) x.UniqueName, False)
        world.CreateRoute(newName, routeType, fromLocation, verge, toLocation)
    End Sub

    Private Sub RunAddEntrance(world As World, toLocation As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim routeType = PickThingie(Of RouteType)("Route Type:", world.RouteTypes, Function(x) x.UniqueName, False)
        Dim verge = PickThingie(Of Verge)("Verge:", world.Verges, Function(x) x.UniqueName, False)
        Dim fromLocation = PickThingie(Of Location)("From Location:", world.Locations, Function(x) x.UniqueName, False)
        world.CreateRoute(newName, routeType, fromLocation, verge, toLocation)
    End Sub

    Private Sub RunAddItem(world As World, location As Location)
        Dim itemType = PickThingie(Of ItemType)("What Item Type?", world.ItemTypes, Function(x) x.UniqueName, True)
        If itemType IsNot Nothing Then
            location.Inventory.Add(world.CreateItem(itemType))
        End If
    End Sub

    Private Sub RunRemoveItem(location As Location)
        Dim item = PickThingie(Of Item)("Which Item?", location.Items, Function(x) x.UniqueName, True)
        If item IsNot Nothing Then
            item.Destroy()
        End If
    End Sub

    Private Sub RunChangeName(location As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            location.Name = newName
        End If
    End Sub
End Module
