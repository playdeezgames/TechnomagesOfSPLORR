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

    Public Function Read(locationTypeId As Long, statisticTypeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long, Long)(
            TableName,
            StatisticDeltaColumn,
            (LocationTypeIdColumn, locationTypeId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Function

    Public Sub Write(locationTypeId As Long, statisticTypeId As Long, value As Long)
        Store.ReplaceRecord(
            TableName,
            (LocationTypeIdColumn, locationTypeId),
            (StatisticTypeIdColumn, statisticTypeId),
            (StatisticDeltaColumn, value))
    End Sub

    Public Sub Clear(locationTypeId As Long, statisticTypeId As Long)
        Store.ClearForColumnValues(
            TableName,
            (LocationTypeIdColumn, locationTypeId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Sub
End Class
