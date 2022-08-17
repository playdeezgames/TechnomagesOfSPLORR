Public Class Item
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long) As Item
        Return New Item(worldData, id)
    End Function

    Public ReadOnly Property Name As String
        Get
            Return ItemType.Name
        End Get
    End Property
    Public ReadOnly Property ItemType As ItemType
        Get
            Return ItemType.FromId(WorldData, WorldData.Item.ReadItemType(Id).Value)
        End Get
    End Property
End Class
