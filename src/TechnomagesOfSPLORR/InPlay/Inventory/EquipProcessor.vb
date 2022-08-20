Module EquipProcessor
    Friend Sub Run(character As Character, item As Item)
        If Not character.CanEquip(item) Then
            AnsiConsole.MarkupLine($"{character.FullName} cannot equip {item.Name}.")
            Return
        End If
        character.Equip(item)
        AnsiConsole.MarkupLine($"{character.FullName} equips {item.Name}.")
    End Sub
End Module
