﻿Module CharacterTypeProcessor
    Friend Sub RunEdit(characterType As CharacterType)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Character Type:")
            AnsiConsole.MarkupLine($"Id: {characterType.Id}")
            AnsiConsole.MarkupLine($"Name: {characterType.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If characterType.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeNameText
                    RunChangeName(characterType)
                Case DeleteText
                    RunDelete(characterType)
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunDelete(characterType As CharacterType)
        characterType.Destroy()
    End Sub

    Friend Sub RunNew(world As World)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            world.CreateCharacterType(newName)
        End If
    End Sub

    Private Sub RunChangeName(characterType As CharacterType)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            characterType.Name = newName
        End If
    End Sub
End Module
