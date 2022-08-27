Module CharacterProcessor
    Friend Sub RunEdit(world As World, character As Character)
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
                prompt.AddChoice(RemoveItemText)
            End If
            prompt.AddChoice(EquipItemText)
            If character.HasEquipment Then
                prompt.AddChoice(UnequipItemText)
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
                    RunAddChangeStatistic(world, character)
                Case AddItemText
                    RunAddItem(world, character)
                Case ChangeCharacterTypeText
                    RunChangeCharacterType(world, character)
                Case ChangeLocationText
                    RunChangeLocation(world, character)
                Case ChangeNameText
                    RunChangeName(character)
                Case DeleteText
                    character.Destroy()
                    Exit Do
                Case EquipItemText
                    RunEquipItem(world, character)
                Case GoBackText
                    Exit Do
                Case LeaveTeamText
                    character.Leave()
                Case JoinTeamText
                    character.Join()
                Case RemoveItemText
                    RunRemoveItem(character)
                Case RemoveStatisticText
                    RunRemoveStatistic(character)
                Case ToggleCanJoinText
                    character.CanJoin = Not character.CanJoin
                Case ToggleCanLeaveText
                    character.CanLeave = Not character.CanLeave
                Case UnequipItemText
                    RunUnequipItem(character)
            End Select
        Loop
    End Sub
    Private Sub RunAddChangeStatistic(world As World, character As Character)
        Dim statisticType = PickThingie("Which Statistic?", world.StatisticTypes, Function(x) x.UniqueName, True)
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

    Private Sub RunUnequipItem(character As Character)
        Dim table = character.Equipment.
            GroupBy(Function(x) x.Item2.UniqueName).
            ToDictionary(Function(x) x.Key, Function(x) x.Select(Function(y) y.Item1))
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Unequip What?[/]"}
        prompt.AddChoice(NeverMindText)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Dim sum = "tasty" 'for grahamweldon, just leave this here
                'do nothing
            Case Else
                character.UnequipEquipSlots(table(answer))
        End Select
    End Sub

    Private Sub RunEquipItem(world As World, character As Character)
        Dim itemType = PickThingie("Which Item Type?", world.ItemTypes, Function(x) x.UniqueName, True)
        If itemType Is Nothing Then
            Return
        End If
        Dim item = world.CreateItem(itemType)
        If character.CanEquip(item) Then
            character.Equip(item)
        Else
            AnsiConsole.MarkupLine($"You cannot equip {itemType.UniqueName} on {character.UniqueName}.")
            item.Destroy()
            OkPrompt()
        End If
    End Sub

    Private Sub RunRemoveItem(character As Character)
        Dim item = PickThingie(Of Item)("Which Item?", character.Items, Function(x) x.UniqueName, True)
        If item IsNot Nothing Then
            item.Destroy()
        End If
    End Sub

    Private Sub RunAddItem(world As World, character As Character)
        Dim itemType = PickThingie(Of ItemType)("What Item Type?", world.ItemTypes, Function(x) x.UniqueName, True)
        If itemType IsNot Nothing Then
            character.Inventory.Add(world.CreateItem(itemType))
        End If
    End Sub

    Private Sub RunChangeCharacterType(world As World, character As Character)
        Dim characterType = PickThingie("Which Character Type?", world.CharacterTypes, Function(x) x.UniqueName, True)
        If characterType IsNot Nothing Then
            character.CharacterType = characterType
        End If
    End Sub

    Private Sub RunChangeLocation(world As World, character As Character)
        Dim location = PickThingie("Which Location?", world.Locations, Function(x) x.UniqueName, True)
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
        RunEdit(world, world.CreateCharacter(newName, characterType, location))
    End Sub
End Module
