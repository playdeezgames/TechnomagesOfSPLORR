﻿Public Class ItemTypeEquipSlotData
    Inherits BaseData
    Friend Const TableName = "ItemTypeEquipSlots"
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForEquipSlot(equipSlotId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (EquipSlotIdColumn, equipSlotId))
    End Function
End Class