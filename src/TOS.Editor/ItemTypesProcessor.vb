Module ItemTypesProcessor
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Item Type?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewText)
            Dim table = world.ItemTypes.ToDictionary(Function(x) x.UniqueName, Function(x) x)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case NewText
                    'RunNew(world)
                Case Else
                    RunEdit(world, table(answer))
            End Select
        Loop
    End Sub
    Private Sub RunEdit(world As World, itemType As ItemType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Item Type:")
            AnsiConsole.MarkupLine($"* Id: {itemType.Id}")
            AnsiConsole.MarkupLine($"* Name: {itemType.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeDisplayNameText)
            If itemType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    'RunChangeName(EquipSlot)
                Case DeleteText
                    'RunDelete(EquipSlot)
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub
End Module
