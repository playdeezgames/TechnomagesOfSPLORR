Module ItemProcessor
    Friend Sub RunEdit(item As Item)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Item:")
            AnsiConsole.MarkupLine($"Id: {item.Id}")
            AnsiConsole.MarkupLine($"Type: {item.ItemType.UniqueName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(NeverMindText)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case NeverMindText
                    Exit Do
            End Select
        Loop
    End Sub
End Module
