Module CharacterProcessor
    Friend Sub RunEdit(character As Character)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Character:")
            AnsiConsole.MarkupLine($"* Id: {character.Id}")
            AnsiConsole.MarkupLine($"* Name: {character.Name}")
            AnsiConsole.MarkupLine($"* Type: {character.CharacterType.UniqueName}")
            AnsiConsole.MarkupLine($"* Location: {character.Location.UniqueName}")
            AnsiConsole.MarkupLine($"* Team:")
            AnsiConsole.MarkupLine($"  * Can Join: {character.CanJoin}")
            If character.CanJoin Then
                AnsiConsole.MarkupLine($"  * On Team: {character.OnTheTeam}")
                AnsiConsole.MarkupLine($"  * Can Leave: {character.CanLeave}")
            End If
            If character.HasItems Then
                AnsiConsole.MarkupLine($"* Items:")
                For Each item In character.Items
                    AnsiConsole.MarkupLine($"  * {item.UniqueName}")
                Next
            End If
            If character.HasEquipment Then
                AnsiConsole.MarkupLine($"* Equipment:")
                For Each item In character.EquippedItems
                    AnsiConsole.MarkupLine($"  * {item.UniqueName}")
                Next
            End If
            If character.HasStatisticDeltas Then
                AnsiConsole.MarkupLine($"* Statistic Deltas:")
                For Each delta In character.StatisticDeltas
                    AnsiConsole.MarkupLine($"  * {delta.Item1.UniqueName}: {delta.Item2}")
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(
                GoBackText,
                ChangeNameText,
                ChangeCharacterTypeText,
                ChangeLocationText)
            If character.OnTheTeam AndAlso character.CanLeave Then
                prompt.AddChoice(LeaveTeamText)
            End If
            If Not character.OnTheTeam AndAlso character.CanJoin Then
                prompt.AddChoice(JoinTeamText)
            End If
            If (character.CanJoin AndAlso Not character.OnTheTeam) OrElse Not character.CanJoin Then
                prompt.AddChoice(ToggleCanJoinText)
            End If
            If character.CanJoin AndAlso character.OnTheTeam Then
                prompt.AddChoice(ToggleCanLeaveText)
            End If
            prompt.AddChoice(AddItemText)
            If character.HasItems Then
                prompt.AddChoice(EditItemText)
            End If
            If character.HasEquipment Then
                prompt.AddChoice(EditEquippedItemText)
            End If
            prompt.AddChoice(AddChangeStatisticText)
            If character.HasStatisticDeltas Then
                prompt.AddChoice(RemoveStatisticText)
            End If
            If character.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddChangeStatisticText
                    RunAddChangeStatistic(character)
                Case AddItemText
                    RunAddItem(character)
                Case ChangeCharacterTypeText
                    RunChangeCharacterType(character)
                Case ChangeLocationText
                    RunChangeLocation(character)
                Case ChangeNameText
                    RunChangeName(character)
                Case DeleteText
                    character.Destroy()
                    Exit Do
                Case EditEquippedItemText
                    RunEditEquippedItem(character)
                Case EditItemText
                    RunEditItem(character)
                Case GoBackText
                    Exit Do
                Case LeaveTeamText
                    character.Leave()
                Case JoinTeamText
                    character.Join()
                Case RemoveStatisticText
                    RunRemoveStatistic(character)
                Case ToggleCanJoinText
                    character.CanJoin = Not character.CanJoin
                Case ToggleCanLeaveText
                    character.CanLeave = Not character.CanLeave
            End Select
        Loop
    End Sub
    Private Sub RunEditItem(character As Character)
        Dim item = PickThingie("Which Item?", character.Items, Function(x) x.UniqueName, True)
        If item IsNot Nothing Then
            ItemProcessor.RunEdit(item)
        End If
    End Sub
    Private Sub RunEditEquippedItem(character As Character)
        Dim item = PickThingie("Which Equipped Item?", character.EquippedItems, Function(x) x.UniqueName, True)
        If item IsNot Nothing Then
            ItemProcessor.RunEdit(item)
        End If
    End Sub
    Private Sub RunAddChangeStatistic(character As Character)
        Dim statisticType = PickThingie("Which Statistic?", character.World.StatisticTypes, Function(x) x.UniqueName, True)
        If statisticType Is Nothing Then
            Return
        End If
        Dim delta = AnsiConsole.Ask(Of Long)("[olive]Statistic Delta?[/]")
        character.StatisticDelta(statisticType) = delta
    End Sub

    Private Sub RunRemoveStatistic(character As Character)
        Dim statisticType = PickThingie("Which Statistic?", character.StatisticDeltas.Select(Function(x) x.Item1), Function(x) x.UniqueName, True)
        If statisticType IsNot Nothing Then
            character.StatisticDelta(statisticType) = Nothing
        End If
    End Sub

    Private Sub RunAddItem(character As Character)
        Dim itemType = PickThingie(Of ItemType)("What Item Type?", character.World.ItemTypes, Function(x) x.UniqueName, True)
        If itemType IsNot Nothing Then
            Dim item = character.World.CreateItem(itemType)
            character.Inventory.Add(item)
            ItemProcessor.RunEdit(item)
        End If
    End Sub

    Private Sub RunChangeCharacterType(character As Character)
        Dim characterType = PickThingie("Which Character Type?", character.World.CharacterTypes, Function(x) x.UniqueName, True)
        If characterType IsNot Nothing Then
            character.CharacterType = characterType
        End If
    End Sub

    Private Sub RunChangeLocation(character As Character)
        Dim location = PickThingie("Which Location?", character.World.Locations, Function(x) x.UniqueName, True)
        If location IsNot Nothing Then
            character.Location = location
        End If
    End Sub

    Private Sub RunChangeName(character As Character)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            character.Name = newName
        End If
    End Sub

    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Character?",
            Function(x) x.Characters,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub

    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim characterType = PickThingie("Character Type:", world.CharacterTypes, Function(x) x.UniqueName, False)
        Dim location = PickThingie("Location:", world.Locations, Function(x) x.UniqueName, False)
        RunEdit(world.CreateCharacter(newName, characterType, location))
    End Sub
End Module
