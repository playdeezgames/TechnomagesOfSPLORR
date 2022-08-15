Module EditProcessor
    Sub Run()
        Dim done = False
        Dim world As New World(BoilerplateDb)
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Edit Menu[/]"}
            prompt.AddChoices(
                EditLocationsText,
                CommitChangesText,
                AbandonChangesText,
                ResetText)
            Select Case AnsiConsole.Prompt(prompt)
                Case EditLocationsText
                    EditLocationsProcessor.Run(world)
                Case CommitChangesText
                    world.Save(BoilerplateDb)
                    done = True
                Case AbandonChangesText
                    done = ConfirmProcessor.Run("Are you sure you want to abandon yer changes?")
                Case ResetText
                    If ConfirmProcessor.Run("Are you sure you want to nuke the world?") Then
                        world.Reset()
                    End If
            End Select
        End While
    End Sub
End Module
