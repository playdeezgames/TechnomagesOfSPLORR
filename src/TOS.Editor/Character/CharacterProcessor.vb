﻿Module CharacterProcessor
    Friend Sub RunEdit(world As World, character As Character)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("Character:")
            AnsiConsole.MarkupLine($"* Id: {character.Id}")
            AnsiConsole.MarkupLine($"* Name: {character.Name}")
            AnsiConsole.MarkupLine($"* Type: {character.CharacterType.UniqueName}")
            AnsiConsole.MarkupLine($"* Location: {character.Location.UniqueName}")
            AnsiConsole.MarkupLine($"* Team:")
            AnsiConsole.MarkupLine($"  * Can Join: {character.CanJoin}")
            If character.CanJoin Then
                AnsiConsole.MarkupLine($"  * On Team: {character.OnTheTeam}")
                AnsiConsole.MarkupLine($"  * Can Leave: {character.CanLeave}")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(
                GoBackText,
                ChangeNameText,
                ChangeCharacterTypeText,
                ChangeLocationText)
            If character.OnTheTeam AndAlso character.CanLeave Then
                prompt.AddChoice(LeaveTeamText)
            End If
            If Not character.OnTheTeam AndAlso character.CanJoin Then
                prompt.AddChoice(JoinTeamText)
            End If
            If (character.CanJoin AndAlso Not character.OnTheTeam) OrElse Not character.CanJoin Then
                prompt.AddChoice(ToggleCanJoinText)
            End If
            If character.CanJoin AndAlso character.OnTheTeam Then
                prompt.AddChoice(ToggleCanLeaveText)
            End If
            If character.CanDelete Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeCharacterTypeText
                    RunChangeCharacterType(world, character)
                Case ChangeLocationText
                    RunChangeLocation(world, character)
                Case ChangeNameText
                    RunChangeName(character)
                Case DeleteText
                    character.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
                Case LeaveTeamText
                    character.Leave()
                Case JoinTeamText
                    character.Join()
                Case ToggleCanJoinText
                    character.CanJoin = Not character.CanJoin
                Case ToggleCanLeaveText
                    character.CanLeave = Not character.CanLeave
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
        Dim newName = AnsiConsole.Ask("[olive]New Name:[/]", "")
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        Dim characterType = PickThingie("Character Type:", world.CharacterTypes, Function(x) x.UniqueName, False)
        Dim location = PickThingie("Location:", world.Locations, Function(x) x.UniqueName, False)
        RunEdit(world, world.CreateCharacter(newName, characterType, location))
    End Sub
End Module
