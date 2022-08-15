Imports System

Module Program
    Sub Main(args As String())
        HandleSplash()
        HandleMainMenu()
    End Sub

    Private Sub HandleMainMenu()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case QuitText
                    done = ConfirmQuit()
            End Select
        End While
    End Sub

    Private Function ConfirmQuit() As Boolean
        AnsiConsole.MarkupLine("[red]Are you sure you want to quit?[/]")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoices(NoText, YesText)
        Return AnsiConsole.Prompt(prompt) = YesText
    End Function

    Private Sub HandleSplash()
        AnsiConsole.Clear()
        Dim title As New FigletText("Technomages of SPLORR!!") With {.Color = Color.Grey, .Alignment = Justify.Center}
        AnsiConsole.Write(title)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Ok")
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
