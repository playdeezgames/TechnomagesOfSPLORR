Public Class ItemTypeEquipSlotData
    Inherits BaseData
    Friend Const TableName = "ItemTypeEquipSlots"
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForEquipSlot(equipSlotId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (EquipSlotIdColumn, equipSlotId))
    End Function

    Public Function CountForItemType(itemTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (ItemTypeIdColumn, itemTypeId))
    End Function
End Class
