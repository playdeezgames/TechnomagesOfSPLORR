Module EditLocationsProcessor
    Friend Sub Run(world As World)
        Dim done = False
        While Not done
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "Locations:"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewLocationText)
            Dim table = world.Locations.ToDictionary(Function(x) x.UniqueName, Function(x) x)
            prompt.AddChoices(table.Keys.ToArray)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case NewLocationText
                    NewLocationProcessor.Run(world)
                Case Else
                    EditLocationProcessor.Run(table(answer))
            End Select
        End While
    End Sub
End Module
