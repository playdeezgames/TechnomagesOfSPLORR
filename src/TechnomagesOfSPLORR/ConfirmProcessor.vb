Module ConfirmProcessor
    Function Run(text As String) As Boolean
        AnsiConsole.MarkupLine($"[red]{text}[/]")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoices(NoText, YesText)
        Return AnsiConsole.Prompt(prompt) = YesText
    End Function
End Module
