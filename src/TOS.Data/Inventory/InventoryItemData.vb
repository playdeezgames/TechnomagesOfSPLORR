Public Class InventoryItemData
    Inherits BaseData
    Friend Const TableName = "InventoryItems"
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
    Friend Const ItemIdColumn = ItemData.ItemIdColumn

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
End Class
