Module EquipSlotsProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Equip Slot?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewText)
            Dim table = world.EquipSlots.ToDictionary(Function(x) x.UniqueName, Function(x) x)
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

    Private Sub RunEdit(world As World, equipSlot As EquipSlot)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Equip Slot:")
            AnsiConsole.MarkupLine($"* Id: {equipSlot.Id}")
            AnsiConsole.MarkupLine($"* Name: {equipSlot.Name}")
            AnsiConsole.MarkupLine($"* Display Name: {equipSlot.DisplayName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeDisplayNameText)
            If equipSlot.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeDisplayNameText
                    RunChangeDisplayName(equipSlot)
                Case ChangeNameText
                    RunChangeName(equipSlot)
                Case DeleteText
                    RunDelete(equipSlot)
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunChangeDisplayName(equipSlot As EquipSlot)
        Dim newName = AnsiConsole.Ask("[olive]New Display Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            equipSlot.DisplayName = newName
        End If
    End Sub

    Private Sub RunChangeName(equipSlot As EquipSlot)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            equipSlot.Name = newName
        End If
    End Sub

    Private Sub RunDelete(equipSlot As EquipSlot)
        equipSlot.Destroy()
    End Sub

    Private Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim newDisplayName = AnsiConsole.Ask("[olive]New Display Name:[/]", "")
        If String.IsNullOrWhiteSpace(newDisplayName) Then
            Return
        End If
        RunEdit(world, world.CreateEquipSlot(newName, newDisplayName))
    End Sub

End Module
