Public Class ItemData
    Inherits BaseData
    Friend Const TableName = "Items"
    Friend Const ItemIdColumn = "ItemId"
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadItemType(itemId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, ItemTypeIdColumn, (ItemIdColumn, itemId))
    End Function

    Public Function CountForItemType(itemTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(
            TableName,
            (ItemTypeIdColumn, itemTypeId))
    End Function
End Class
