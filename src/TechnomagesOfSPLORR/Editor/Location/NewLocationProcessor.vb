Module NewLocationProcessor
    Friend Sub Run(world As World)
        Dim name = AnsiConsole.Ask(Of String)("[olive]Location Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(name) Then
            world.AddLocation(name)
        End If
    End Sub
End Module
