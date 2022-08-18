Module Utility
    Friend Const AbandonGameText = "Abandon Game"
    Friend Const BoilerplateDb = "boilerplate.db"
    Friend Const DropText = "Drop"
    Friend Const EmbarkText = "Embark!"
    Friend Const GoBackText = "Go Back"
    Friend Const InventoryText = "Inventory"
    Friend Const MoveText = "Move"
    Friend Const NeverMindText = "Never Mind"
    Friend Const NoText = "No"
    Friend Const OkText = "Ok"
    Friend Const QuitText = "Quit"
    Friend Const TakeText = "Take"
    Friend Const YesText = "Yes"
    Friend Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
    Friend Function ChooseCharacter(text As String, characters As IEnumerable(Of Character)) As Character
        If Not characters.Any Then
            Return Nothing
        End If
        If characters.Count = 1 Then
            Return characters.Single
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{text}[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = characters.ToDictionary(Function(x) x.FullName, Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return table(answer)
        End Select
    End Function

    Friend Function ChooseQuantity(text As String, count As Integer) As Integer
        If count = 1 Then
            Return count
        End If
        Dim prompt As New SelectionPrompt(Of Integer) With {.Title = $"[olive]{text}[/]"}
        For index = 0 To count
            prompt.AddChoices(index)
        Next
        Return AnsiConsole.Prompt(prompt)
    End Function

    Friend Function ChooseItemStack(text As String, itemStacks As IEnumerable(Of (ItemType, IEnumerable(Of Item)))) As IEnumerable(Of Item)
        If Not itemStacks.Any Then
            Return Array.Empty(Of Item)
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{text}[/]"}
        Dim table = itemStacks.ToDictionary(Function(x) $"{x.Item1.Name}(x{x.Item2.Count})", Function(x) x.Item2)
        table(NeverMindText) = Array.Empty(Of Item)
        prompt.AddChoices(table.Keys)
        Dim items = table(AnsiConsole.Prompt(prompt))
        Return items
    End Function
End Module
