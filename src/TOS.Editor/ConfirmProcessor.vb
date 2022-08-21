Module ConfirmProcessor
    Friend Function Run(text As String) As Boolean
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[red]{text}[/]"}
        prompt.AddChoices(NoText, YesText)
        Return AnsiConsole.Prompt(prompt) = YesText
    End Function
End Module
