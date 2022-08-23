Public Class ItemType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long) As ItemType
        Return New ItemType(worldData, id)
    End Function

    Public ReadOnly Property Name() As String
        Get
            Return WorldData.ItemType.ReadName(Id)
        End Get
    End Property

    Public ReadOnly Property UniqueName() As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return WorldData.Item.CountForItemType(Id) = 0
        End Get
    End Property
End Class
