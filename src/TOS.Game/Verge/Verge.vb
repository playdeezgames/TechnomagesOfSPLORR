Public Class Verge
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub

    Public Shared Function FromId(worldData As WorldData, id As Long) As Verge
        Return New Verge(worldData, id)
    End Function

    Public Property Name As String
        Get
            Return WorldData.Verge.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.Verge.WriteName(Id, value)
        End Set
    End Property

    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public Property VergeType As VergeType
        Get
            Return VergeType.FromId(WorldData, WorldData.Verge.ReadVergeType(Id).Value)
        End Get
        Set(value As VergeType)
            WorldData.Verge.WriteVergeType(Id, value.Id)
        End Set
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return WorldData.Route.CountForVerge(Id) = 0
        End Get
    End Property

    Public Sub Destroy()
        WorldData.Verge.Clear(Id)
    End Sub
End Class
