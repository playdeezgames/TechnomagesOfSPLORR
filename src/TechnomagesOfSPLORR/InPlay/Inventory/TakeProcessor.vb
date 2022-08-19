Module TakeProcessor
    Friend Sub Run(world As World)
        Do
            Dim characters = world.Team.Characters
            Dim itemStacks = world.Team.Location.ItemStacks
            Dim items As IEnumerable(Of Item) = ChooseItemStack("Take what?", itemStacks)
            If Not items.Any Then
                Exit Do
            End If
            Dim itemCount = ChooseQuantity("How many?", items.Count)
            If itemCount > 0 Then
                Dim character = ChooseCharacter("Who takes it?", characters)
                If character IsNot Nothing Then
                    character.Inventory.Add(items.Take(itemCount))
                End If
            End If
        Loop
    End Sub
End Module
