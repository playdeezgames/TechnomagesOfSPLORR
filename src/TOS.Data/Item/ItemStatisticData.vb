Public Class ItemStatisticData
    Inherits BaseData
    Friend Const ViewName = "ItemStatistics"
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const StatisticDeltaColumn = "StatisticDelta"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function Read(itemId As Long, statisticTypeId As Long) As Long?
        Return Store.ReadColumnValue(
            Of Long, Long, Long)(
            ViewName,
            StatisticDeltaColumn,
            (ItemIdColumn, itemId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Function
End Class
