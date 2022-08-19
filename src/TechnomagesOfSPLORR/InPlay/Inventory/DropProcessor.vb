Module DropProcessor

    Friend Sub Run(itemStack As IEnumerable(Of Item), location As Location)
        location.Inventory.Add(itemStack.Take(ChooseQuantity("Drop how many?", itemStack.Count)))
    End Sub

End Module
