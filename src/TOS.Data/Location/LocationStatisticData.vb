Public Class LocationStatisticData
    Inherits BaseData
    Friend Const TableName = "LocationStatistics"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const StatisticDeltaColumn = "StatisticDelta"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForLocation(locationId As Long) As Long
        Return Store.ReadCountForColumnValue(
            TableName,
            (LocationIdColumn, locationId))
    End Function

    Public Function ReadForLocation(locationId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            TableName,
            (StatisticTypeIdColumn, StatisticDeltaColumn),
            (LocationIdColumn, locationId))
    End Function

    Public Function Read(locationId As Long, statisticTypeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long, Long)(
            TableName,
            StatisticDeltaColumn,
            (StatisticTypeIdColumn, statisticTypeId),
            (LocationIdColumn, locationId))
    End Function

    Public Sub Write(locationId As Long, statisticTypeId As Long, delta As Long)
        Store.ReplaceRecord(
            TableName,
            (LocationIdColumn, locationId),
            (StatisticTypeIdColumn, statisticTypeId),
            (StatisticDeltaColumn, delta))
    End Sub

    Public Sub Clear(locationId As Long, statisticTypeId As Long)
        Store.ClearForColumnValues(
            TableName,
            (LocationIdColumn, locationId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Sub
End Class
