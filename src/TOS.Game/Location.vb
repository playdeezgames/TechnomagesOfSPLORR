Public Class Location
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    Shared Function FromId(locationId As Long) As Location
        Return New Location(locationId)
    End Function

    Public ReadOnly Property UniqueName As String
        Get
            Return $"({Id})"
        End Get
    End Property
End Class
