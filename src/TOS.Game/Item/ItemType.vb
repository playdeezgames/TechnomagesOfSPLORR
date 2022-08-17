Public Class ItemType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long) As ItemType
        Return New ItemType(worldData, id)
    End Function

    Friend Function Name() As String
        Return WorldData.ItemType.ReadName(Id)
    End Function
End Class
