Public Class ItemTypeEquipSlotData
    Inherits BaseData
    Friend Const TableName = "ItemTypeEquipSlots"
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn
    Friend Const AlternativeColumn = "Alternative"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForEquipSlot(equipSlotId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (EquipSlotIdColumn, equipSlotId))
    End Function

    Public Function CountForItemType(itemTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (ItemTypeIdColumn, itemTypeId))
    End Function

    Public Function ReadForItemType(itemTypeId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(TableName, (AlternativeColumn, EquipSlotIdColumn), (ItemTypeIdColumn, itemTypeId))
    End Function

    Public Sub Clear(itemTypeId As Long, alternative As Long, equipSlotId As Long)
        Store.ClearForColumnValues(
            TableName,
            (ItemTypeIdColumn, itemTypeId),
            (AlternativeColumn, alternative),
            (EquipSlotIdColumn, equipSlotId))
    End Sub

    Public Sub Write(itemTypeId As Long, alternative As Long, equipSlotId As Long)
        Store.ReplaceRecord(
            TableName,
            (ItemTypeIdColumn, itemTypeId),
            (AlternativeColumn, alternative),
            (EquipSlotIdColumn, equipSlotId))
    End Sub
End Class
