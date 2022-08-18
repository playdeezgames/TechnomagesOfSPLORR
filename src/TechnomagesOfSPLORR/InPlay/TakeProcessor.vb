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
                    Dim itemCount = ChooseQuantity(items.Count)
                    If itemCount > 0 Then
                        Dim character = ChooseCharacter(characters)
                        If character IsNot Nothing Then
                            character.Inventory.Add(items.Take(itemCount))
                        End If
                    End If
            End Select
        End While
    End Sub

    Private Function ChooseCharacter(characters As IEnumerable(Of Character)) As Character
        If characters.Count = 1 Then
            Return characters.Single
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Who Takes It?[/]"}
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

    Private Function ChooseQuantity(count As Integer) As Integer
        If count = 1 Then
            Return count
        End If
        Dim prompt As New SelectionPrompt(Of Integer) With {.Title = "[olive]How Many?[/]"}
        For index = 0 To count
            prompt.AddChoices(index)
        Next
        Return AnsiConsole.Prompt(prompt)
    End Function
End Module
