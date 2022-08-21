Module MainProcessor
    Friend Sub Run()
        AnsiConsole.Clear()
        Dim world As New World(BoilerplateDb)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case QuitText
                    If ConfirmProcessor.Run("Are you sure you want to quit without saving?") Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module
