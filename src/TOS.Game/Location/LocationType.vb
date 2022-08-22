Public Class LocationType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, locationTypeId As Long)
        MyBase.New(worldData, locationTypeId)
    End Sub
    Public Shared Function FromId(worldData As WorldData, locationTypeId As Long) As LocationType
        Return New LocationType(worldData, locationTypeId)
    End Function

    Public ReadOnly Property Name As String
        Get
            Return WorldData.LocationType.ReadName(Id)
        End Get
    End Property

    Public ReadOnly Property UniqueName() As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public Function CanDelete() As Boolean
        Return WorldData.LocationTypeStatistic.CountForLocationType(Id) = 0 AndAlso
            WorldData.Location.CountForLocationType(Id) = 0
    End Function

    Public ReadOnly Property HasStatistics() As Boolean
        Get
            Return WorldData.LocationTypeStatistic.CountForLocationType(Id) > 0
        End Get
    End Property
    Public ReadOnly Property Statistics As IEnumerable(Of (StatisticType, Long))
        Get
            Return WorldData.LocationTypeStatistic.ReadForLocationType(Id).
                Select(Function(x) (StatisticType.FromId(WorldData, x.Item1), x.Item2))
        End Get
    End Property
End Class
