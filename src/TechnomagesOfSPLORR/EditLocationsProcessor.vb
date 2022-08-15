Module EditLocationsProcessor
    Friend Sub Run(world As World)
        Dim done = False
        While Not done
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "Locations:"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewLocationText)
            For Each location In world.Locations
                prompt.AddChoice(location.UniqueName)
            Next
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case NewLocationText
                    NewLocationProcessor.Run(world)
                Case Else
            End Select
        End While
    End Sub
End Module
