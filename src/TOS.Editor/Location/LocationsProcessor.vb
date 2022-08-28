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
        RunEdit(world.CreateLocation(newName, locationType))
    End Sub
    Private Sub RunEdit(location As Location)
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
            If location.HasStatisticDeltas Then
                AnsiConsole.MarkupLine($"* Statistic Deltas:")
                For Each delta In location.StatisticDeltas
                    AnsiConsole.MarkupLine($"  * {delta.Item1.UniqueName}: {delta.Item2}")
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
            prompt.AddChoice(AddCharacterText)
            If location.HasCharacters Then
                prompt.AddChoice(EditCharacterText)
            End If
            prompt.AddChoice(AddItemText)
            If location.HasItems Then
                prompt.AddChoice(EditItemText)
                prompt.AddChoice(RemoveItemText)
            End If
            prompt.AddChoice(AddChangeStatisticText)
            If location.HasStatisticDeltas Then
                prompt.AddChoice(RemoveStatisticText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddChangeStatisticText
                    RunAddChangeStatistic(location)
                Case AddCharacterText
                    RunAddCharacter(location)
                Case AddEntranceText
                    RunAddEntrance(location)
                Case AddExitText
                    RunAddExit(location)
                Case AddItemText
                    RunAddItem(location)
                Case ChangeNameText
                    RunChangeName(location)
                Case DeleteText
                    location.Destroy()
                    Exit Do
                Case EditCharacterText
                    RunEditCharacter(location)
                Case EditEntranceText
                    RunEditEntrance(location)
                Case EditExitText
                    RunEditExit(location)
                Case EditItemText
                    RunEditItem(location)
                Case GoBackText
                    Exit Do
                Case RemoveEntranceText
                    RunRemoveEntrance(location)
                Case RemoveExitText
                    RunRemoveExit(location)
                Case RemoveItemText
                    RunRemoveItem(location)
                Case RemoveStatisticText
                    RunRemoveStatistic(location)
            End Select
        Loop
    End Sub
    Private Sub RunEditItem(location As Location)
        Dim item = PickThingie("Which Item?", location.Items, Function(x) x.UniqueName, True)
        If item IsNot Nothing Then
            ItemProcessor.RunEdit(item)
        End If
    End Sub
    Private Sub RunAddChangeStatistic(location As Location)
        Dim statisticType = PickThingie("Which Statistic?", location.World.StatisticTypes, Function(x) x.UniqueName, True)
        If statisticType Is Nothing Then
            Return
        End If
        Dim delta = AnsiConsole.Ask(Of Long)("[olive]Statistic Delta?[/]")
        location.StatisticDelta(statisticType) = delta
    End Sub

    Private Sub RunRemoveStatistic(location As Location)
        Dim statisticType = PickThingie("Which Statistic?", location.StatisticDeltas.Select(Function(x) x.Item1), Function(x) x.UniqueName, True)
        If statisticType IsNot Nothing Then
            location.StatisticDelta(statisticType) = Nothing
        End If
    End Sub

    Private Sub RunAddCharacter(location As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim characterType = PickThingie("Character Type:", location.World.CharacterTypes, Function(x) x.UniqueName, False)
        CharacterProcessor.RunEdit(location.World.CreateCharacter(newName, characterType, location))
    End Sub

    Private Sub RunEditCharacter(location As Location)
        Dim character = PickThingie("Which Character?", location.Characters, Function(x) x.UniqueName, True)
        If character IsNot Nothing Then
            CharacterProcessor.RunEdit(character)
        End If
    End Sub

    Private Sub RunEditExit(location As Location)
        Dim route = PickThingie("Which Exit?", location.Exits, Function(x) x.UniqueName, True)
        If route IsNot Nothing Then
            RouteProcessor.RunEdit(route)
        End If
    End Sub

    Private Sub RunEditEntrance(location As Location)
        Dim route = PickThingie("Which Exit?", location.Entrances, Function(x) x.UniqueName, True)
        If route IsNot Nothing Then
            RouteProcessor.RunEdit(route)
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

    Private Sub RunAddExit(fromLocation As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim routeType = PickThingie(Of RouteType)("Route Type:", fromLocation.World.RouteTypes, Function(x) x.UniqueName, False)
        Dim verge = PickThingie(Of Verge)("Verge:", fromLocation.World.Verges, Function(x) x.UniqueName, False)
        Dim toLocation = PickThingie(Of Location)("To Location:", fromLocation.World.Locations, Function(x) x.UniqueName, False)
        fromLocation.World.CreateRoute(newName, routeType, fromLocation, verge, toLocation)
    End Sub

    Private Sub RunAddEntrance(toLocation As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim routeType = PickThingie(Of RouteType)("Route Type:", toLocation.World.RouteTypes, Function(x) x.UniqueName, False)
        Dim verge = PickThingie(Of Verge)("Verge:", toLocation.World.Verges, Function(x) x.UniqueName, False)
        Dim fromLocation = PickThingie(Of Location)("From Location:", toLocation.World.Locations, Function(x) x.UniqueName, False)
        toLocation.World.CreateRoute(newName, routeType, fromLocation, verge, toLocation)
    End Sub

    Private Sub RunAddItem(location As Location)
        Dim itemType = PickThingie(Of ItemType)("What Item Type?", location.World.ItemTypes, Function(x) x.UniqueName, True)
        If itemType IsNot Nothing Then
            location.Inventory.Add(location.World.CreateItem(itemType))
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
