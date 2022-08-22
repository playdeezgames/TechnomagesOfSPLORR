Public Class LocationTypeStatisticData
    Inherits BaseData
    Friend Const TableName = "LocationTypeStatistics"
    Friend Const LocationTypeIdColumn = LocationTypeData.LocationTypeIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const StatisticDeltaColumn = "StatisticDelta"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForStatisticType(statisticTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(
            TableName,
            (StatisticTypeIdColumn, statisticTypeId))
    End Function

    Public Function CountForLocationType(locationTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(
            TableName,
            (LocationTypeIdColumn, locationTypeId))
    End Function
    Public Function ReadForLocationType(locationTypeId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            TableName,
            (StatisticTypeIdColumn, StatisticDeltaColumn),
            (LocationTypeIdColumn, locationTypeId))
    End Function

End Class
