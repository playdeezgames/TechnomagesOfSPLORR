Module SplashProcessor
    Sub Run()
        AnsiConsole.Clear()
        Dim title As New FigletText("Technomages of SPLORR!!") With {.Color = Color.Grey, .Alignment = Justify.Center}
        AnsiConsole.Write(title)
        OkPrompt()
    End Sub
End Module
