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

    Public ReadOnly Property ToLocation As Location
        Get
            Return Location.FromId(WorldData, WorldData.Route.ReadToLocationId(Id).Value)
        End Get
    End Property

    Public ReadOnly Property FromLocation As Location
        Get
            Return Location.FromId(WorldData, WorldData.Route.ReadFromLocationId(Id).Value)
        End Get
    End Property

    Public ReadOnly Property Verge As Verge
        Get
            Return Verge.FromId(WorldData, WorldData.Route.ReadVergeId(Id).Value)
        End Get
    End Property


    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public Sub Destroy()
        WorldData.Route.Clear(Id)
    End Sub
End Class
