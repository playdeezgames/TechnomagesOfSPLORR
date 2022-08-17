Public Class LocationType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, locationTypeId As Long)
        MyBase.New(worldData, locationTypeId)
    End Sub
    Public Shared Function FromId(worldData As WorldData, locationTypeId As Long) As LocationType
        Return New LocationType(worldData, locationTypeId)
    End Function
End Class
