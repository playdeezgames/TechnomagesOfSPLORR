Module ItemProcessor
    Friend Sub RunEdit(item As Item)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Item:")
            AnsiConsole.MarkupLine($"* Id: {item.Id}")
            AnsiConsole.MarkupLine($"* Type: {item.ItemType.UniqueName}")
            If item.IsEquipped Then
                AnsiConsole.MarkupLine("* Equipped On:")
                For Each entry In item.EquippedOn
                    AnsiConsole.MarkupLine($"  * {entry.Item1.UniqueName}: {entry.item2.UniqueName}")
                Next
            End If
            If item.HasLocation Then
                AnsiConsole.MarkupLine($"* Location: {item.Location.UniqueName}")
            End If
            If item.HasCharacter Then
                AnsiConsole.MarkupLine($"* Character: {item.Character.UniqueName}")
            End If
            If item.HasStatisticDeltas Then
                AnsiConsole.MarkupLine($"* Statistic Deltas:")
                For Each delta In item.StatisticDeltas
                    AnsiConsole.MarkupLine($"  * {delta.Item1.UniqueName}: {delta.Item2}")
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(NeverMindText)
            prompt.AddChoice(ChangeItemTypeText)
            If item.IsEquipped Then
                prompt.AddChoice(UnequipItemText)
            Else
                prompt.AddChoice(EquipItemText)
            End If
            prompt.AddChoice(SetLocationText)
            prompt.AddChoice(SetCharacterText)
            prompt.AddChoice(AddChangeStatisticText)
            If item.HasStatisticDeltas Then
                prompt.AddChoice(RemoveStatisticText)
            End If
            If item.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case AddChangeStatisticText
                    RunAddChangeStatistic(item)
                Case ChangeItemTypeText
                    RunChangeItemType(item)
                Case DeleteText
                    item.Destroy()
                    Exit Do
                Case EquipItemText
                    RunEquipItem(item)
                Case NeverMindText
                    Exit Do
                Case RemoveStatisticText
                    RunRemoveStatistic(item)
                Case SetCharacterText
                    RunSetCharacter(item)
                Case SetLocationText
                    RunSetLocation(item)
                Case UnequipItemText
                    RunUnequipItem(item)
            End Select
        Loop
    End Sub
    Private Sub RunAddChangeStatistic(item As Item)
        Dim statisticType = PickThingie("Which Statistic?", item.World.StatisticTypes, Function(x) x.UniqueName, True)
        If statisticType Is Nothing Then
            Return
        End If
        Dim delta = AnsiConsole.Ask(Of Long)("[olive]Statistic Delta?[/]")
        item.StatisticDelta(statisticType) = delta
    End Sub

    Private Sub RunRemoveStatistic(item As Item)
        Dim statisticType = PickThingie("Which Statistic?", item.StatisticDeltas.Select(Function(x) x.Item1), Function(x) x.UniqueName, True)
        If statisticType IsNot Nothing Then
            item.StatisticDelta(statisticType) = Nothing
        End If
    End Sub
    Private Sub RunChangeItemType(item As Item)
        Dim itemType = PickThingie("Which Item Type?", item.World.ItemTypes, Function(x) x.UniqueName, True)
        If itemType IsNot Nothing Then
            item.ItemType = itemType
        End If
    End Sub
    Private Sub RunSetLocation(item As Item)
        Dim location = PickThingie("Which Location?", item.World.Locations, Function(x) x.UniqueName, True)
        If location IsNot Nothing Then
            item.Unequip()
            location.Inventory.Add(item)
        End If
    End Sub
    Private Sub RunSetCharacter(item As Item)
        Dim character = PickThingie("Which Character?", item.World.Characters, Function(x) x.UniqueName, True)
        If character IsNot Nothing Then
            item.Unequip()
            character.Inventory.Add(item)
        End If
    End Sub
    Private Sub RunEquipItem(item As Item)
        Dim character = PickThingie("Which Character?", item.World.Characters.Where(Function(x) x.CanEquip(item)), Function(x) x.UniqueName, True)
        If character IsNot Nothing Then
            character.Equip(item)
        End If
    End Sub

    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Item?",
            Function(x) x.Items,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub

    Private Sub RunUnequipItem(item As Item)
        item.Unequip()
    End Sub

    Private Sub RunNew(world As World)
        Dim itemType = PickThingie("Which Item Type?", world.ItemTypes, Function(x) x.UniqueName, True)
        If itemType IsNot Nothing Then
            RunEdit(world.CreateItem(itemType))
        End If
    End Sub
End Module
