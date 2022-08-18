Module TakeProcessor
    Friend Sub Run(world As World)
        Dim done = False
        While Not done
            Dim characters = world.Team.Characters
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Take What?[/]"}
            prompt.AddChoice(NeverMindText)
            Dim itemStacks = world.Team.Location.ItemStacks
            If Not itemStacks.Any Then
                Return
            End If
            Dim table = itemStacks.ToDictionary(Function(x) $"{x.Item1.Name}(x{x.Item2.Count})", Function(x) x.Item2)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case NeverMindText
                    done = True
                Case Else
                    Dim items = table(answer)
                    Dim itemCount = ChooseQuantity("How many?", items.Count)
                    If itemCount > 0 Then
                        Dim character = ChooseCharacter("Who takes it?", characters)
                        If character IsNot Nothing Then
                            character.Inventory.Add(items.Take(itemCount))
                        End If
                    End If
            End Select
        End While
    End Sub
End Module
