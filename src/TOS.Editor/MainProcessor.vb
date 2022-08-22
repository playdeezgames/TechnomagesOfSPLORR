Module MainProcessor
    Friend Sub Run()
        AnsiConsole.Clear()
        Dim world As New World(BoilerplateDb)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            prompt.AddChoice(CharacterTypesText)
            prompt.AddChoice(EquipSlotsText)
            prompt.AddChoice(LocationTypesText)
            prompt.AddChoice(StatisticTypesText)
            prompt.AddChoice(SaveAndQuitText)
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case CharacterTypesText
                    CharacterTypesProcessor.Run(world)
                Case EquipSlotsText
                    EquipSlotsProcessor.Run(world)
                Case LocationTypesText
                    LocationTypesProcessor.Run(world)
                Case QuitText
                    If ConfirmProcessor.Run("Are you sure you want to quit without saving?") Then
                        Exit Do
                    End If
                Case SaveAndQuitText
                    world.Save(BoilerplateDb)
                    Exit Do
                Case StatisticTypesText
                    StatisticTypesProcessor.Run(world)
            End Select
        Loop
    End Sub
End Module
