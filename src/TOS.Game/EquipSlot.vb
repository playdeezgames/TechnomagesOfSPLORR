Public Class EquipSlot
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long) As EquipSlot
        Return New EquipSlot(worldData, id)
    End Function
End Class
