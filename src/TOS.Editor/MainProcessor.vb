Module MainProcessor
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
                Case RouteTypesText
                    RouteTypesProcessor.Run(world)
                Case SaveAndQuitText
                    world.Save(BoilerplateDb)
                    Exit Do
                Case StatisticTypesText
                    StatisticTypesProcessor.Run(world)
                Case VergesText
                    VergesProcessor.Run(world)
                Case VergeTypesText
                    VergeTypesProcessor.Run(world)
            End Select
        Loop
    End Sub
End Module
