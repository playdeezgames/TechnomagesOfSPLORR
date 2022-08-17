Public Class Location
    Inherits BaseThingie
    Sub New(worldData As WorldData, locationId As Long)
        MyBase.New(worldData, locationId)
    End Sub
    Shared Function FromId(data As WorldData, locationId As Long) As Location
        Return New Location(data, locationId)
    End Function
    Public Property Name As String
        Get
            Return WorldData.Location.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.Location.WriteName(Id, value)
        End Set
    End Property
    Public ReadOnly Property Routes As IEnumerable(Of Route)
        Get
            Return WorldData.Route.ReadForLocationId(Id).Select(Function(x) Route.FromId(WorldData, x))
        End Get
    End Property
    Public ReadOnly Property RouteNames As String
        Get
            Return String.Join(", ", Routes.Select(Function(x) x.Name))
        End Get
    End Property
    Public Property LocationType As LocationType
        Get
            Return LocationType.FromId(WorldData, WorldData.Location.ReadLocationType(Id).Value)
        End Get
        Set(value As LocationType)
            WorldData.Location.WriteLocationType(Id, value.Id)
        End Set
    End Property
End Class
