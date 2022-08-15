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
            Return $"{Name}({Id})"
        End Get
    End Property
    Public Property Name As String
        Get
            Return WorldData.Location.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.Location.WriteName(Id, value)
        End Set
    End Property

    Public Sub Destroy()
        WorldData.Location.Clear(Id)
    End Sub
End Class
