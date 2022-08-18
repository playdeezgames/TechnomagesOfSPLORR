Module InventoryProcessor
    Friend Sub Run(world As World)
        Dim done = False
        While Not done
            Dim character = ChooseCharacter("Which team member?", world.Team.Characters)
            If character Is Nothing Then
                Return
            End If
        End While
    End Sub
End Module
