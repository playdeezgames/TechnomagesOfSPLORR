Public Class ConditionType
    Inherits BaseThingie

    Public Sub New(world As World, id As Long)
        MyBase.New(world, id)
    End Sub
    Public Shared Function FromId(world As World, id As Long) As ConditionType
        Return New ConditionType(world, id)
    End Function
    Public Property Name As String
        Get
            Return WorldData.ConditionType.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.ConditionType.WriteName(Id, value)
        End Set
    End Property
    Public Property DisplayName As String
        Get
            Return WorldData.ConditionType.ReadDisplayName(Id)
        End Get
        Set(value As String)
            WorldData.ConditionType.WriteDisplayName(Id, value)
        End Set
    End Property
    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property CanDelete() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Sub Destroy()
        WorldData.ConditionType.Clear(Id)
    End Sub
End Class
