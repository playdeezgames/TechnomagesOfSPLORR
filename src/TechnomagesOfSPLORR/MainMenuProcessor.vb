Module MainMenuProcessor
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            prompt.AddChoice(EmbarkText)
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case EmbarkText
                    InPlayProcessor.Run(New World(BoilerplateDb))
                Case QuitText
                    done = ConfirmProcessor.Run("Are you sure you want to quit?")
            End Select
        End While
    End Sub
End Module
