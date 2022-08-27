Public Class VergeType
    Inherits BaseThingie

    Public Sub New(world as World, id As Long)
        MyBase.New(world, id)
    End Sub
    Public Shared Function FromId(world As World, id As Long) As VergeType
        Return New VergeType(world, id)
    End Function

    Public Property Name As String
        Get
            Return WorldData.VergeType.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.VergeType.WriteName(Id, value)
        End Set
    End Property

    Public ReadOnly Property UniqueName() As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return WorldData.Verge.CountForVergeType(Id) = 0
        End Get
    End Property

    Public Sub Destroy()
        WorldData.VergeType.Clear(Id)
    End Sub
End Class
