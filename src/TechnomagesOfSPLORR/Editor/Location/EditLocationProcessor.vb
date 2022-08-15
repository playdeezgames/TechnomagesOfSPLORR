Module EditLocationProcessor
    Friend Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Location Id: {location.Id}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
            End Select
        End While
    End Sub
End Module
