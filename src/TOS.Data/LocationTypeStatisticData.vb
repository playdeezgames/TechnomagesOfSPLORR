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
End Class
