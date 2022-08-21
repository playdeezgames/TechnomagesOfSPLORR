Imports System

Module Program
    Sub Main(args As String())
        AnsiConsole.Clear()
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case QuitText
                    Exit Do
            End Select
        Loop
    End Sub
End Module
