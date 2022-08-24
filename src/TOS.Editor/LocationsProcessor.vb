Module LocationsProcessor
    Friend Sub Run(world As World)
        RunList(
            world,
            "Which Location?",
            Function(x) x.Locations,
            Function(x) x.UniqueName,
            AddressOf RunNew,
            AddressOf RunEdit)
    End Sub
    Private Sub RunNew(world As World)
        'Throw New NotImplementedException
    End Sub
    Private Sub RunEdit(world As World, location As Location)
        'Throw New NotImplementedException
    End Sub
End Module
