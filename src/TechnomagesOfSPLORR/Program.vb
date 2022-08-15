Imports System

Module Program
    Sub Main(args As String())
        Dim title As New FigletText("Technomages of SPLORR!!") With {.Color = Color.Grey, .Alignment = Justify.Center}
        AnsiConsole.Write(title)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Ok")
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
