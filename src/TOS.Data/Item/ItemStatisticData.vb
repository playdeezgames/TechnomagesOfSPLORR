Public Class ItemStatisticData
    Inherits BaseData
    Friend Const TableName = "ItemStatistics"
    Friend Const ItemIdColumn = "ItemId"
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const StatisticDeltaColumn = "StatisticDelta"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForItem(itemId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (ItemIdColumn, itemId))
    End Function

    Public Function ReadForCharacter(itemId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            TableName,
            (StatisticTypeIdColumn, StatisticDeltaColumn),
            (ItemIdColumn, itemId))
    End Function

    Public Function Read(itemId As Long, statisticTypeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long, Long)(
            TableName,
            StatisticDeltaColumn,
            (ItemIdColumn, itemId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Function

    Public Sub Write(itemId As Long, statisticTypeId As Long, delta As Long)
        Store.ReplaceRecord(
            TableName,
            (ItemIdColumn, itemId),
            (StatisticTypeIdColumn, statisticTypeId),
            (StatisticDeltaColumn, delta))
    End Sub

    Public Sub Clear(itemId As Long, statisticTypeId As Long)
        Store.ClearForColumnValues(
            TableName,
            (ItemIdColumn, itemId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Sub
End Class
