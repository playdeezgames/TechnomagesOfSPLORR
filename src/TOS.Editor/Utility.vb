Module Utility
    Friend Sub RunList(Of TThingie)(
                                   world As World,
                                   title As String,
                                   source As Func(Of World, IEnumerable(Of TThingie)),
                                   keySource As Func(Of TThingie, String),
                                   newRunner As Action(Of World),
                                   editRunner As Action(Of World, TThingie))
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewText)
            Dim table = source(world).ToDictionary(keySource, Function(x) x)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case NewText
                    newRunner(world)
                Case Else
                    editRunner(world, table(answer))
            End Select
        Loop
    End Sub

End Module
