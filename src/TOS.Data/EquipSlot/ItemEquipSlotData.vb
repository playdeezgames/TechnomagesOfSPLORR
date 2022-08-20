Public Class ItemEquipSlotData
    Inherits BaseData
    Friend Const ViewName = "ItemEquipSlots"
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn
    Friend Const AlternativeColumn = "Alternative"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadForItem(itemId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            ViewName,
            (AlternativeColumn, EquipSlotIdColumn),
            (ItemIdColumn, itemId))
    End Function
End Class
