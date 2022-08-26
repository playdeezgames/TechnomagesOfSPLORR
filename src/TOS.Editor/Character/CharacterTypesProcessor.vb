Module CharacterTypesProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Character Type?",
            Function(x) x.CharacterTypes,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunEdit(world As World, characterType As CharacterType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Character Type:")
            AnsiConsole.MarkupLine($"* Id: {characterType.Id}")
            AnsiConsole.MarkupLine($"* Name: {characterType.Name}")
            Dim statistics = characterType.Statistics
            If statistics.Any Then
                AnsiConsole.MarkupLine($"* Statistic Deltas:")
                For Each statistic In statistics
                    AnsiConsole.MarkupLine($"  * {statistic.Item1.Name}({statistic.Item1.DisplayName}): {statistic.Item2}")
                Next
            End If
            Dim equipSlots = characterType.EquipSlots
            If equipSlots.Any Then
                AnsiConsole.MarkupLine($"* Equip Slots:")
                For Each equipSlot In equipSlots
                    AnsiConsole.MarkupLine($"  * {equipSlot.Name}({equipSlot.DisplayName})")
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If characterType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            prompt.AddChoice(AddChangeStatisticText)
            If characterType.HasStatistics Then
                prompt.AddChoice(RemoveStatisticText)
            End If
            prompt.AddChoice(AddEquipSlotText)
            If characterType.HasEquipSlots Then
                prompt.AddChoice(RemoveEquipSlotText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddChangeStatisticText
                    RunAddChangeStatistic(world, characterType)
                Case AddEquipSlotText
                    RunAddEquipSlotText(world, characterType)
                Case ChangeNameText
                    RunChangeName(characterType)
                Case DeleteText
                    RunDelete(characterType)
                    Exit Do
                Case GoBackText
                    Exit Do
                Case RemoveEquipSlotText
                    RunRemoveEquipSlot(characterType)
                Case RemoveStatisticText
                    RunRemoveStatistic(characterType)
            End Select
        Loop
    End Sub

    Private Sub RunRemoveEquipSlot(characterType As CharacterType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Equip Slot?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = characterType.EquipSlots.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                characterType.RemoveEquipSlot(table(answer))
        End Select
    End Sub

    Private Sub RunAddEquipSlotText(world As World, characterType As CharacterType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Equip Slot?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = world.EquipSlots.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                characterType.AddEquipSlot(table(answer))
        End Select
    End Sub

    Private Sub RunRemoveStatistic(characterType As CharacterType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Remove which statistic?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = characterType.Statistics.ToDictionary(Of String, StatisticType)(Function(x) x.Item1.UniqueName, Function(x) x.Item1)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                characterType.Statistic(table(answer)) = Nothing
        End Select
    End Sub

    Private Sub RunAddChangeStatistic(world As World, characterType As CharacterType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Add/Change which statistic?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = world.StatisticTypes.ToDictionary(Of String, StatisticType)(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                Dim statisticValue = AnsiConsole.Ask(Of Long)("[olive]Statistic Value: [/]")
                characterType.Statistic(table(answer)) = statisticValue
        End Select
    End Sub

    Private Sub RunDelete(characterType As CharacterType)
        characterType.Destroy()
    End Sub

    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            RunEdit(world, world.CreateCharacterType(newName))
        End If
    End Sub

    Private Sub RunChangeName(characterType As CharacterType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            characterType.Name = newName
        End If
    End Sub
End Module
