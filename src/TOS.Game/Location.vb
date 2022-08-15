Public Class Location
    Inherits BaseThingie
    ReadOnly Property Id As Long
    Sub New(worldData As WorldData, locationId As Long)
        MyBase.New(worldData)
        Id = locationId
    End Sub
    Shared Function FromId(data As WorldData, locationId As Long) As Location
        Return New Location(data, locationId)
    End Function

    Public ReadOnly Property UniqueName As String
        Get
            Return $"({Id})"
        End Get
    End Property
End Class
