Public Class Route
    Inherits BaseThingie

    Public Sub New(world as World, id As Long)
        MyBase.New(world, id)
    End Sub
    Public Shared Function FromId(world As World, id As Long) As Route
        Return New Route(world, id)
    End Function
    Public Property Name As String
        Get
            Return WorldData.Route.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.Route.WriteName(Id, value)
        End Set
    End Property
    Public Property RouteType As RouteType
        Get
            Return RouteType.FromId(World, WorldData.Route.ReadRouteType(Id).Value)
        End Get
        Set(value As RouteType)
            WorldData.Route.WriteRouteType(Id, value.Id)
        End Set
    End Property

    Public Property ToLocation As Location
        Get
            Return Location.FromId(World, WorldData.Route.ReadToLocationId(Id).Value)
        End Get
        Set(value As Location)
            WorldData.Route.WriteToLocation(Id, value.Id)
        End Set
    End Property

    Public Property FromLocation As Location
        Get
            Return Location.FromId(World, WorldData.Route.ReadFromLocationId(Id).Value)
        End Get
        Set(value As Location)
            WorldData.Route.WriteFromLocation(Id, value.Id)
        End Set
    End Property

    Public Property Verge As Verge
        Get
            Return Verge.FromId(World, WorldData.Route.ReadVergeId(Id).Value)
        End Get
        Set(value As Verge)
            WorldData.Route.WriteVerge(Id, value.Id)
        End Set
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
