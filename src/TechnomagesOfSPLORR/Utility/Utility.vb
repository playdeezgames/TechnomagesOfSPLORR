﻿Module Utility
    Friend Const AbandonGameText = "Abandon Game"
    Friend Const BoilerplateDb = "boilerplate.db"
    Friend Const EmbarkText = "Embark!"
    Friend Const GoBackText = "Go Back"
    Friend Const InventoryText = "Inventory"
    Friend Const MoveText = "Move"
    Friend Const NeverMindText = "Never Mind"
    Friend Const NoText = "No"
    Friend Const QuitText = "Quit"
    Friend Const TakeText = "Take"
    Friend Const YesText = "Yes"
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
End Module
