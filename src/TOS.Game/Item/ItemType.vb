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

    Public Property Statistic(statisticType As StatisticType) As Long?
        Get
            Return WorldData.ItemTypeStatistic.Read(Id, statisticType.Id)
        End Get
        Set(value As Long?)
            If value.HasValue Then
                WorldData.ItemTypeStatistic.Write(Id, statisticType.Id, value.Value)
            Else
                WorldData.ItemTypeStatistic.Clear(Id, statisticType.Id)
            End If
        End Set
    End Property
End Class
