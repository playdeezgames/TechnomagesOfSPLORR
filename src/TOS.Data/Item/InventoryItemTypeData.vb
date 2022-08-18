Public Class InventoryItemTypeData
    Inherits BaseData
    Friend Const ViewName = "InventoryItemTypes"
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn
    Friend Const ItemIdColumn = ItemData.ItemIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadItemStacks(inventoryId As Long) As IEnumerable(Of (Long, IEnumerable(Of Long)))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            ViewName,
            (ItemTypeIdColumn, ItemIdColumn),
            (InventoryIdColumn, inventoryId)).
            GroupBy(Function(x) x.Item1).
            Select(Function(x) (x.Key, x.Select(Function(y) y.Item2)))
    End Function
End Class
