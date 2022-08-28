Module EquipSlotsProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Equip Slot?",
            Function(x) x.EquipSlots,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub

    Private Sub RunEdit(equipSlot As EquipSlot)
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
        RunEdit(world.CreateEquipSlot(newName, newDisplayName))
    End Sub

End Module
