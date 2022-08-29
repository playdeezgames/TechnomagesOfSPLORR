Module MainProcessor
    Private ReadOnly table As IReadOnlyDictionary(Of String, Action(Of World)) =
        New Dictionary(Of String, Action(Of World)) From
        {
            {CharactersText, AddressOf CharacterProcessor.Run},
            {CharacterTypesText, AddressOf CharacterTypesProcessor.Run},
            {ConditionTypesText, AddressOf ConditionTypesProcessor.Run},
            {EquipSlotsText, AddressOf EquipSlotsProcessor.Run},
            {ItemsText, AddressOf ItemProcessor.Run},
            {ItemTypesText, AddressOf ItemTypesProcessor.Run},
            {LocationsText, AddressOf LocationsProcessor.Run},
            {LocationTypesText, AddressOf LocationTypesProcessor.Run},
            {RouteTypesText, AddressOf RouteTypesProcessor.Run},
            {StatisticTypesText, AddressOf StatisticTypesProcessor.Run},
            {VergesText, AddressOf VergesProcessor.Run},
            {VergeTypesText, AddressOf VergeTypesProcessor.Run}
        }
    Friend Sub Run()
        AnsiConsole.Clear()
        Dim world As New World(BoilerplateDb)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            prompt.AddChoices(table.Keys)
            prompt.AddChoice(SaveAndQuitText)
            prompt.AddChoice(QuitText)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case QuitText
                    If ConfirmProcessor.Run("Are you sure you want to quit without saving?") Then
                        Exit Do
                    End If
                Case SaveAndQuitText
                    world.Save(BoilerplateDb)
                    Exit Do
                Case Else
                    table(answer).Invoke(world)
            End Select
        Loop
    End Sub
End Module
