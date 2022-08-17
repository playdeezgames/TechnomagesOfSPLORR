Public Class InventoryItemData
    Inherits BaseData
    Friend Const TableName = "InventoryItems"
    Friend Const InventoryItemTypesViewName = "InventoryItemTypes"
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn
    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadItemCount(inventoryId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (InventoryIdColumn, inventoryId))
    End Function

    Public Function ReadForInventory(inventoryId As Long) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValue(Of Long, Long)(
            TableName,
            ItemIdColumn,
            (InventoryIdColumn, inventoryId))
    End Function

    Public Function ReadItemStacks(inventoryId As Long) As IEnumerable(Of (Long, IEnumerable(Of Long)))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            InventoryItemTypesViewName,
            (ItemTypeIdColumn, ItemIdColumn),
            (InventoryIdColumn, inventoryId)).
            GroupBy(Function(x) x.Item1).
            Select(Function(x) (x.Key, x.Select(Function(y) y.Item2)))
    End Function
End Class
