Public Class EquipSlotData
    Inherits BaseData
    Implements IProvidesAll
    Friend Const TableName = "EquipSlots"
    Friend Const EquipSlotIdColumn = "EquipSlotId"
    Friend Const EquipSlotDisplayNameColumn = "EquipSlotDisplayName"
    Friend Const EquipSlotNameColumn = "EquipSlotName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadDisplayName(equipSlotId As Long) As String
        Return Store.ReadColumnString(TableName, EquipSlotDisplayNameColumn, (EquipSlotIdColumn, equipSlotId))
    End Function

    Public Sub WriteDisplayName(equipSlotId As Long, displayName As String)
        Store.WriteColumnValue(TableName, (EquipSlotDisplayNameColumn, displayName), (EquipSlotIdColumn, equipSlotId))
    End Sub

    Public Function ReadName(equipSlotId As Long) As String
        Return Store.ReadColumnString(TableName, EquipSlotNameColumn, (EquipSlotIdColumn, equipSlotId))
    End Function

    Public Sub WriteName(equipSlotId As Long, name As String)
        Store.WriteColumnValue(TableName, (EquipSlotNameColumn, name), (EquipSlotIdColumn, equipSlotId))
    End Sub

    Public Function All() As IEnumerable(Of Long) Implements IProvidesAll.All
        Return Store.ReadRecords(Of Long)(TableName, EquipSlotIdColumn)
    End Function

    Public Sub Clear(equipSlotId As Long)
        Store.ClearForColumnValue(TableName, (EquipSlotIdColumn, equipSlotId))
    End Sub

    Public Function Create(newName As String, newDisplayName As String) As Long
        Return Store.CreateRecord(TableName, (EquipSlotNameColumn, newName), (EquipSlotDisplayNameColumn, newDisplayName))
    End Function
End Class
