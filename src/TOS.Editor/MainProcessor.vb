Module MainProcessor
    Private ReadOnly table As IReadOnlyDictionary(Of String, Action(Of World)) =
        New Dictionary(Of String, Action(Of World)) From
        {
            {CharacterTypesText, AddressOf CharacterTypesProcessor.Run},
            {EquipSlotsText, AddressOf EquipSlotsProcessor.Run},
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
            prompt.AddChoice(CharacterTypesText)
            prompt.AddChoice(EquipSlotsText)
            prompt.AddChoice(LocationTypesText)
            prompt.AddChoice(RouteTypesText)
            prompt.AddChoice(StatisticTypesText)
            prompt.AddChoice(VergesText)
            prompt.AddChoice(VergeTypesText)
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
