Module EditLocationProcessor
    Friend Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Location Id: {location.Id}")
            AnsiConsole.MarkupLine($"Location Name: {location.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(RenameText)
            prompt.AddChoice(DeleteText)
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
                Case DeleteText
                    done = ConfirmDeleteLocation(location, done)
                Case RenameText
                    RenameLocation(location)
            End Select
        End While
    End Sub

    Private Sub RenameLocation(location As Location)
        Dim newName = AnsiConsole.Ask("[olive]New Name?[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            location.Name = newName
        End If
    End Sub

    Private Function ConfirmDeleteLocation(location As Location, done As Boolean) As Boolean
        If ConfirmProcessor.Run($"Are you sure you want to delete `{location.UniqueName}`?") Then
            location.Destroy()
            done = True
        End If

        Return done
    End Function
End Module
