Module ItemTypesProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Item Type?",
            Function(x) x.ItemTypes,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            RunEdit(world.CreateItemType(newName))
        End If
    End Sub
    Private Sub RunEdit(itemType As ItemType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Item Type:")
            AnsiConsole.MarkupLine($"* Id: {itemType.Id}")
            AnsiConsole.MarkupLine($"* Name: {itemType.Name}")

            If itemType.HasStatistics Then
                AnsiConsole.MarkupLine($"* Statistics:")
                For Each entry In itemType.Statistics
                    AnsiConsole.MarkupLine($"  * {entry.Item1.UniqueName}: {entry.Item2}")
                Next
            End If

            If itemType.HasEquipSlots Then
                AnsiConsole.MarkupLine($"* Equip Slots:")
                For Each entry In itemType.EquipSlots
                    AnsiConsole.MarkupLine($"  * {entry.Item1}:{entry.Item2.UniqueName}")
                Next
            End If

            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeDisplayNameText)
            If itemType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            prompt.AddChoice(AddChangeStatisticText)
            If itemType.HasStatistics Then
                prompt.AddChoice(RemoveStatisticText)
            End If
            prompt.AddChoice(AddEquipSlotText)
            If itemType.HasEquipSlots Then
                prompt.AddChoice(RemoveEquipSlotText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case AddChangeStatisticText
                    RunAddChangeStatistic(itemType)
                Case AddEquipSlotText
                    RunAddEquipSlotText(itemType)
                Case ChangeNameText
                    RunChangeName(itemType)
                Case DeleteText
                    RunDelete(itemType)
                    Exit Do
                Case GoBackText
                    Exit Do
                Case RemoveEquipSlotText
                    RunRemoveEquipSlot(itemType)
                Case RemoveStatisticText
                    RunRemoveStatistic(itemType)
            End Select
        Loop
    End Sub

    Private Sub RunRemoveEquipSlot(itemType As ItemType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Equip Slot?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = itemType.EquipSlots.ToDictionary(Function(x) $"{x.Item1}:{x.Item2.UniqueName}", Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                itemType.RemoveEquipSlot(table(answer).Item1, table(answer).Item2)
        End Select
    End Sub

    Private Sub RunAddEquipSlotText(itemType As ItemType)
        Dim alternative = AnsiConsole.Ask(Of Long)("[olive]Which Alternative?[/]")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Equip Slot?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = itemType.World.EquipSlots.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                itemType.AddEquipSlot(alternative, table(answer))
        End Select
    End Sub

    Private Sub RunRemoveStatistic(itemType As ItemType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Remove which statistic?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = itemType.Statistics.ToDictionary(Of String, StatisticType)(Function(x) x.Item1.UniqueName, Function(x) x.Item1)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                itemType.Statistic(table(answer)) = Nothing
        End Select
    End Sub

    Private Sub RunAddChangeStatistic(itemType As ItemType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Add/Change which statistic?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = itemType.World.StatisticTypes.ToDictionary(Of String, StatisticType)(Function(x) x.UniqueName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                Dim statisticValue = AnsiConsole.Ask(Of Long)("[olive]Statistic Value: [/]")
                itemType.Statistic(table(answer)) = statisticValue
        End Select
    End Sub
    Private Sub RunChangeName(itemType As ItemType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            itemType.Name = newName
        End If
    End Sub

    Private Sub RunDelete(itemType As ItemType)
        itemType.Destroy()
    End Sub
End Module
