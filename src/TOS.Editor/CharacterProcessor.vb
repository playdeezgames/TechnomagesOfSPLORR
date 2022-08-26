Module CharacterProcessor
    Friend Sub RunEdit(world As World, character As Character)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Character:")
            AnsiConsole.MarkupLine($"Id: {character.Id}")
            AnsiConsole.MarkupLine($"Name: {character.Name}")
            AnsiConsole.MarkupLine($"Type: {character.CharacterType.UniqueName}")
            AnsiConsole.MarkupLine($"Location: {character.Location.UniqueName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(
                GoBackText,
                ChangeNameText,
                ChangeCharacterTypeText,
                ChangeLocationText)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeCharacterTypeText
                    RunChangeCharacterType(world, character)
                Case ChangeLocationText
                    RunChangeLocation(world, character)
                Case ChangeNameText
                    RunChangeName(character)
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunChangeCharacterType(world As World, character As Character)
        Dim characterType = PickThingie("Which Character Type?", world.CharacterTypes, Function(x) x.UniqueName, True)
        If characterType IsNot Nothing Then
            character.CharacterType = characterType
        End If
    End Sub

    Private Sub RunChangeLocation(world As World, character As Character)
        Dim location = PickThingie("Which Location?", world.Locations, Function(x) x.UniqueName, True)
        If location IsNot Nothing Then
            character.Location = location
        End If
    End Sub

    Private Sub RunChangeName(character As Character)
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(newName) Then
            character.Name = newName
        End If
    End Sub

    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Character?",
            Function(x) x.Characters,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub

    Private Sub RunNew(world As World)

    End Sub
End Module
