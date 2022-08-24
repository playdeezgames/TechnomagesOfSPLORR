Public Class ItemTypeStatisticData
    Inherits BaseData
    Friend Const TableName = "ItemTypeStatistics"
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn
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

    Public Function CountForItemType(itemTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(
            TableName,
            (ItemTypeIdColumn, itemTypeId))
    End Function

    Public Function ReadForItemType(itemTypeId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            TableName,
            (StatisticTypeIdColumn, StatisticDeltaColumn),
            (ItemTypeIdColumn, itemTypeId))
    End Function
End Class
