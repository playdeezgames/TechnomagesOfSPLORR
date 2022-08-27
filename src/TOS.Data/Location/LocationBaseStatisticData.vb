Public Class LocationBaseStatisticData
    Inherits BaseData
    Friend Const ViewName = "LocationBaseStatistics"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const StatisticDeltaColumn = "StatisticDelta"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function Read(locationId As Long, statisticTypeId As Long) As Long?
        Return Store.ReadColumnValue(
            Of Long, Long, Long)(
            ViewName,
            StatisticDeltaColumn,
            (LocationIdColumn, locationId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Function
End Class
