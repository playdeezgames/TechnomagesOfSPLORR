Public Class RouteType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long) As RouteType
        Return New RouteType(worldData, id)
    End Function

    Public Property Name As String
        Get
            Return WorldData.RouteType.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.RouteType.WriteName(Id, value)
        End Set
    End Property


    Public ReadOnly Property UniqueName() As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return WorldData.Route.CountForRouteType(Id) = 0
        End Get
    End Property

    Public Sub Destroy()
        WorldData.RouteType.Clear(Id)
    End Sub
End Class
