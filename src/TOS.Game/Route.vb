Public Class Route
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long) As Route
        Return New Route(worldData, id)
    End Function
    Public ReadOnly Property Name As String
        Get
            Return WorldData.Route.ReadName(Id)
        End Get
    End Property

    Friend Function ToLocation() As Location
        Return Location.FromId(WorldData, WorldData.Route.ReadToLocationId(Id).Value)
    End Function
End Class
