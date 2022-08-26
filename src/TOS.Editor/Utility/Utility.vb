Module Utility
    Friend Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
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
    Friend Function PickThingie(Of TThingie)(
                                            title As String,
                                            thingies As IEnumerable(Of TThingie),
                                            keySource As Func(Of TThingie, String),
                                            cancelable As Boolean) As TThingie
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If cancelable Then
            prompt.AddChoice(NeverMindText)
        End If
        Dim table = thingies.ToDictionary(Function(x) keySource(x), Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return table(answer)
        End Select
    End Function
End Module
