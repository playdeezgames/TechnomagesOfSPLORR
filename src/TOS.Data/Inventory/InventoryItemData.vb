Public Class InventoryItemData
    Inherits BaseData
    Friend Const TableName = "InventoryItems"
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadItemCount(inventoryId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (InventoryIdColumn, inventoryId))
    End Function

    Public Sub ClearForItem(itemId As Long)
        Store.ClearForColumnValue(TableName, (ItemIdColumn, itemId))
    End Sub

    Public Function ReadForInventory(inventoryId As Long) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValue(Of Long, Long)(
            TableName,
            ItemIdColumn,
            (InventoryIdColumn, inventoryId))
    End Function

    Public Function ReadForItem(itemId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(
            TableName,
            InventoryIdColumn,
            (ItemIdColumn, itemId))
    End Function

    Public Sub Write(itemId As Long, inventoryId As Long)
        Store.ReplaceRecord(
            TableName,
            (ItemIdColumn, itemId),
            (InventoryIdColumn, inventoryId))
    End Sub
End Class
