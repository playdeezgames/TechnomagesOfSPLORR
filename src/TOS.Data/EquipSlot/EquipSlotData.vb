Public Class EquipSlotData
    Inherits BaseData
    Friend Const TableName = "EquipSlots"
    Friend Const EquipSlotIdColumn = "EquipSlotId"
    Friend Const EquipSlotDisplayNameColumn = "EquipSlotDisplayName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadDisplayName(equipSlotId As Long) As String
        Return Store.ReadColumnString(TableName, EquipSlotDisplayNameColumn, (EquipSlotIdColumn, equipSlotId))
    End Function
End Class
