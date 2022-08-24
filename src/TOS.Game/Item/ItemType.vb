Public Class ItemType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long) As ItemType
        Return New ItemType(worldData, id)
    End Function

    Public Property Name() As String
        Get
            Return WorldData.ItemType.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.ItemType.WriteName(Id, value)
        End Set
    End Property

    Public ReadOnly Property UniqueName() As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return WorldData.Item.CountForItemType(Id) = 0 AndAlso
                WorldData.ItemTypeEquipSlot.CountForItemType(Id) = 0 AndAlso
                WorldData.ItemTypeStatistic.CountForItemType(Id) = 0
        End Get
    End Property

    Public ReadOnly Property HasStatistics() As Boolean
        Get
            Return WorldData.ItemTypeStatistic.CountForItemType(Id) > 0
        End Get
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of (StatisticType, Long))
        Get
            Return WorldData.ItemTypeStatistic.ReadForItemType(Id).
                Select(Function(x) (StatisticType.FromId(WorldData, x.Item1), x.Item2))
        End Get
    End Property

    Public Sub Destroy()
        WorldData.ItemType.Clear(Id)
    End Sub
End Class
