Module EquipmentProcessor
    Friend Sub Run(world As World)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("Equipment:")
        Dim character = ChooseCharacter("Which team member?", world.Team.Characters)
        If Not character.HasEquipment Then
            AnsiConsole.MarkupLine($"{character.FullName} has no equipment.")
            OkPrompt()
            Return
        End If
        Dim equipment = character.Equipment
        For Each entry In equipment
            AnsiConsole.MarkupLine($"{entry.Item1.DisplayName}: {entry.Item2.Name}")
        Next
        OkPrompt()
    End Sub
End Module
