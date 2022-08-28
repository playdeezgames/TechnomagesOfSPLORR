Public Class ItemData
    Inherits BaseData
    Implements IProvidesAll
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

    Public Sub Clear(itemId As Long)
        Store.ClearForColumnValue(TableName, (ItemIdColumn, itemId))
    End Sub

    Public Function Create(itemTypeId As Long) As Long
        Return Store.CreateRecord(TableName, (ItemTypeIdColumn, itemTypeId))
    End Function

    Public Function All() As IEnumerable(Of Long) Implements IProvidesAll.All
        Return Store.ReadRecords(Of Long)(TableName, ItemIdColumn)
    End Function
End Class
