Module MoveProcessor
    Friend Sub Run(world As World)
        Dim leader = world.Team.Leader
        Dim routeTable = leader.Location.Routes.ToDictionary(Function(x) x.Name, Function(x) x)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Move Which Way?[/]"}
        prompt.AddChoice(NeverMindText)
        prompt.AddChoices(routeTable.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                Dim route = routeTable(answer)
                world.Team.Move(route)
        End Select
    End Sub
End Module
