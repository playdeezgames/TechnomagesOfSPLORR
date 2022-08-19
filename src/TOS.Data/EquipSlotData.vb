Public Class EquipSlotData
    Inherits BaseData
    Friend Const TableName = "EquipSlots"
    Friend Const EquipSlotIdColumn = "EquipSlotId"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub
End Class
